using Newtonsoft.Json;

namespace Blockchain.Business.Model
{
    public class Transaction
    {
        [JsonProperty]
        public string Sender { get; set; }
        [JsonProperty]
        public string Receiver { get; set; }
        [JsonProperty]
        public double Amount { get; set; }
    }
}