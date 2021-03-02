using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Business.Tests
{
    [TestClass()]
    public class ProoferTests
    {
        [TestMethod()]
        public void GetProofOfWorkTest()
        {
            var p = new Proofer();
            var a = p.GetProofOfWork(17649);
            Assert.AreEqual(1, a);
        }

        [TestMethod()]
        public void ValidateProofTest()
        {
            var p = new Proofer();

            var a = p.ValidateProof(17649, 1);
            Assert.AreEqual(true, a);
        }
    }
}