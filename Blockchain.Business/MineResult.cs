namespace Blockchain.Business
{
    public class MineResult
    {
        public string Message { get; internal set; }
        public int Index { get; internal set; }
        public Transaction[] Transactions { get; internal set; }
        public int Proof { get; internal set; }
        public string PreviousHash { get; internal set; }
    }
}