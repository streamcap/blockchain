using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Blockchain.Business.Model;
using Newtonsoft.Json;

namespace Blockchain.Business
{
    public static class Hasher
    {
        public static string CreateHash(Block block)
        {
            var json = JsonConvert.SerializeObject(block);
            var bytes = Encoding.UTF8.GetBytes(json);
            var hash = SHA256.Create().ComputeHash(bytes);
            return GetHexString(hash);
        }

        public static string GetHexString(byte[] bytes)
        {
            return bytes.Select(b => $"{b:X}").Aggregate(new StringBuilder(), (a, b) => a.Append(b)).ToString();
        }
    }
}
