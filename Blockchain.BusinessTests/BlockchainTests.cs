using Blockchain.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Blockchain.BusinessTests
{
    [TestClass]
    public class BlockchainTests
    {
        private IProofHandler _proofHandler;

        [TestInitialize]
        public void Initialize()
        {
            var proofMock = new Mock<IProofHandler>();
            proofMock.Setup(x => x.GetProofOfWork(It.IsAny<int>())).Returns(1);
            proofMock.Setup(x => x.ValidateProof(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _proofHandler = proofMock.Object;
        }

        [TestMethod]
        public void BlockchainTest()
        {
            var bc = new Business.Blockchain(_proofHandler);
            Assert.AreEqual(1, bc.Chain.Count);
        }

        [TestMethod]
        public void MineBlockTest()
        {
            var bc = new Business.Blockchain(_proofHandler);
            bc.MineBlock();
            Assert.AreEqual(2, bc.Chain.Count);
        }

        [TestMethod]
        public void AddTransactionTest()
        {
            var bc = new Business.Blockchain(_proofHandler);
            bc.AddTransaction("1", "2", 1f);
            bc.MineBlock();
            Assert.AreEqual(2, bc.LastBlock.Transactions.Length);
        }
    }
}