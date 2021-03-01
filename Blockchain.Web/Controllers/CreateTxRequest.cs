using Newtonsoft.Json;

namespace Blockchain.Web.Controllers
{
    public class CreateTxRequest
    {
        [JsonProperty]
        public string Sender { get; set; }
        [JsonProperty]
        public string Receiver { get; set; }
        [JsonProperty]
        public double Amount { get; set; }
    }
}