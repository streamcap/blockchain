using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockchain.Business.Model;

namespace Blockchain.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChainController : ControllerBase
    {
        private readonly Business.Blockchain blockchain;

        public ChainController(Business.Blockchain blockchain)
        {
            this.blockchain = blockchain;
        }

        [HttpGet]
        public void Mine()
        {
            blockchain.MineBlock();
        }

        [HttpPost]
        public void Transaction(string sender, string receiver, double amount)
        {
            blockchain.AddTransaction(sender, receiver, amount);
        }

        [HttpGet]
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

        [HttpGet]
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
