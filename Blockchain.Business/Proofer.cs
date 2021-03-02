using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Business
{
    public class Proofer : IProofer
    {
        public int GetProofOfWork(int lastProof)
        {
            var proof = 0;
            while (!ValidateProof(lastProof, proof))
            {
                proof += 1;
            }
            return proof;
        }

        public bool ValidateProof(int lastproof, int proof)
        {
            var guess = Encoding.UTF8.GetBytes($"{lastproof}{proof}");
            var guessHash = SHA256.Create().ComputeHash(guess);
            var hashLine = Hasher.GetHexString(guessHash);
            var result = hashLine.StartsWith("1111");
            return result;
        }
    }
}
