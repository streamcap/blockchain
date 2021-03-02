using Blockchain.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.BusinessTests
{
    [TestClass]
    public class ProofHandlerTests
    {
        [TestMethod]
        public void GetProofOfWorkTest()
        {
            var p = new ProofHandler();
            var a = p.GetProofOfWork(17649);
            Assert.AreEqual(1, a);
        }

        [TestMethod]
        public void ValidateProofTest()
        {
            var p = new ProofHandler();

            var a = p.ValidateProof(17649, 1);
            Assert.AreEqual(true, a);
        }
    }
}