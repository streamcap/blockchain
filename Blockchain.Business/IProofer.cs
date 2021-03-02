namespace Blockchain.Business
{
    public interface IProofer
    {
        int GetProofOfWork(int lastProof);
        bool ValidateProof(int lastproof, int proof);
    }
}