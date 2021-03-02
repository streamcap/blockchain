using Newtonsoft.Json;

namespace Blockchain.ConsolApp
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