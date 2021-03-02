namespace Blockchain.Business.Tests
{
    internal class ProoferMock : IProofer
    {
        public int GetProofOfWork(int lastProof)
        {
            return 1;
        }

        public bool ValidateProof(int lastproof, int proof)
        {
            return true;
        }
    }
}