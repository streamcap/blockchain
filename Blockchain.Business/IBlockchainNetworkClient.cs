using System.Net.Http;
using System.Threading.Tasks;

namespace Blockchain.Business
{
    public interface IBlockchainNetworkClient
    {
        Task<HttpResponseMessage> GetNodeChain(string node);
    }
}