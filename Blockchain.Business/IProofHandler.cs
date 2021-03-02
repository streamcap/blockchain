namespace Blockchain.Business
{
    public interface IProofHandler
    {
        int GetProofOfWork(int lastProof);
        bool ValidateProof(int lastProof, int proof);
    }
}