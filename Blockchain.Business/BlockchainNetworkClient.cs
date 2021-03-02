using System.Net.Http;
using System.Threading.Tasks;

namespace Blockchain.Business
{

    public class BlockchainNetworkClient : IBlockchainNetworkClient
    {
        private readonly HttpClient _client;

        public BlockchainNetworkClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> GetNodeChain(string node)
        {
            return await _client.GetAsync($"http://{node}/chain");
        }
    }
}