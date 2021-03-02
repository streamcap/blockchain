using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Blockchain.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Blockchain.BusinessTests
{
    [TestClass]
    public class BlockchainTests
    {
        private IProofHandler _proofHandler;
        private IBlockchainNetworkClient _client;

        [TestInitialize]
        public void Initialize()
        {
            var proofMock = new Mock<IProofHandler>();
            proofMock.Setup(x => x.GetProofOfWork(It.IsAny<int>())).Returns(1);
            proofMock.Setup(x => x.ValidateProof(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _proofHandler = proofMock.Object;
            var netClient = new Mock<IBlockchainNetworkClient>();
            var chain = new Business.Blockchain(_proofHandler, _client);
            chain.MineBlock();
            var msg = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(chain.Chain))
            };
            netClient.Setup(x => x.GetNodeChain(It.IsAny<string>()))
                .Returns(Task.FromResult(msg));

            _client = netClient.Object;
        }

        [TestMethod]
        public void BlockchainTest()
        {
            var bc = new Business.Blockchain(_proofHandler, _client);
            Assert.AreEqual(1, bc.Chain.Count);
        }

        [TestMethod]
        public void MineBlockTest()
        {
            var bc = new Business.Blockchain(_proofHandler, _client);
            bc.MineBlock();
            Assert.AreEqual(2, bc.Chain.Count);
        }

        [TestMethod]
        public void AddTransactionTest()
        {
            var bc = new Business.Blockchain(_proofHandler, _client);
            bc.AddTransaction("1", "2", 1f);
            bc.MineBlock();
            Assert.AreEqual(2, bc.LastBlock.Transactions.Length);
        }

        [TestMethod()]
        public void RegisterNodeTest()
        {
            var bc = new Business.Blockchain(_proofHandler, _client);
            bc.RegisterNode("http://localhost/");
            Assert.IsTrue(bc.Nodes.Contains("localhost"));
        }

        [TestMethod()]
        public void ResolveConflictsTest_noNew()
        {
            var bc = new Business.Blockchain(_proofHandler, _client);
            var a = bc.ResolveConflicts().Result;
            Assert.IsFalse(a);
        }

        [TestMethod()]
        public void ResolveConflictsTest_HasNew()
        {
            
            var bc = new Business.Blockchain(_proofHandler, _client);
            bc.RegisterNode("http://localhost/");
            var a = bc.ResolveConflicts().Result;
            Assert.IsTrue(a);
        }

    }
}