using Microsoft.AspNetCore.Mvc;

namespace Blockchain.Web.Controllers
{
    public class BlockchainController : Controller
    {
        private readonly Business.Blockchain _blockChain;

        public BlockchainController()
        {
            _blockChain = new Business.Blockchain();
        }

        [Route("chain")]
        public Business.Blockchain Index()
        {
            return new Business.Blockchain();
        }

        [Route("transactions/new")]
        public int CreateTransaction([FromBody] CreateTxRequest request)
        {
            return _blockChain.AddTransaction(request.Sender, request.Receiver, request.Amount);
        }

        [Route("mine")]
        public JsonResult MineBlock()
        {
            var lastBlock = _blockChain.LastBlock;
            var lastProof = lastBlock.Proof;
            var proof = _blockChain.ProofOfWork(lastProof);
            _blockChain.AddTransaction("0", "Me", 1);
            var previousHash = _blockChain.CreateHash(lastBlock);
            var block = _blockChain.CreateBlock(previousHash, proof);

            var response = new
            {
                Message = "New block forged",
                block.Index,
                block.Transactions,
                block.Proof,
                block.PreviousHash
            };
            return new JsonResult(response);
        }
    }
}