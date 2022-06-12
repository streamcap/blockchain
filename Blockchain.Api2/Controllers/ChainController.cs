using Microsoft.AspNetCore.Mvc;
using Blockchain.Business.Model;

namespace Blockchain.Api2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChainController : Controller
    {
        private readonly Business.Blockchain blockchain;

        public ChainController(Business.Blockchain blockchain)
        {
            this.blockchain = blockchain;
        }

        [HttpGet("mine")]
        public void Mine()
        {
            blockchain.MineBlock();
        }

        [HttpPost("transaction")]
        public void Transaction(string sender, string receiver, double amount)
        {
            blockchain.AddTransaction(sender, receiver, amount);
        }

        [HttpGet("chain")]
        public List<Block> Chain()
        {
            return blockchain.Chain.ToList();
        }

        [HttpPost("register")]
        public List<string> RegisterNodes([FromBody] List<string> nodes)
        {
            foreach (var node in nodes)
            {
                blockchain.RegisterNode(node);
            }

            return blockchain.Nodes.ToList();
        }

        [HttpGet("resolve")]
        public async Task<dynamic> Resolve()
        {
            var replaced = await blockchain.ResolveConflicts();
            var response = new
            {
                Message = replaced ? "Replaced" : "Not replaced",
                blockchain.Chain
            };
            return response;
        }
    }
}
