using System.Text;
using Blockchain.Business;
using Blockchain.Business.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.BusinessTests
{
    [TestClass]
    public class HasherTests
    {
        [TestMethod]
        public void CreateHashTest()
        {
            var a = Hasher.CreateHash(new Block());
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void GetHexStringTest()
        {
            var e = Hasher.GetHexString(Encoding.ASCII.GetBytes("AAA"));
            Assert.AreEqual("414141", e);
        }
    }
}