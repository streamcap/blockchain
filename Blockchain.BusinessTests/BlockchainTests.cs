using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Business.Tests
{
    [TestClass()]
    public class BlockchainTests
    {
        private ProoferMock proofer;

        [TestInitialize]
        public void Initialize()
        {
            proofer = new ProoferMock();
        }

        [TestMethod()]
        public void BlockchainTest()
        {
            var bc = new Blockchain(proofer);
            Assert.AreEqual(1, bc.Chain.Count);
        }

        [TestMethod()]
        public void MineBlockTest()
        {
            var bc = new Blockchain(proofer);
            bc.MineBlock();
            Assert.AreEqual(2, bc.Chain.Count);
        }

        [TestMethod()]
        public void AddTransactionTest()
        {
            var bc = new Blockchain(proofer);
            bc.AddTransaction("1", "2", 1f);
            bc.MineBlock();
            Assert.AreEqual(2, bc.LastBlock.Transactions.Length);
        }
    }
}