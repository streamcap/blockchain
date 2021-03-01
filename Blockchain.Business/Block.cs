using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Blockchain.Business
{
    public class Block
    {
        [JsonProperty]
        public int Index { get; set; }
        [JsonProperty]
        public DateTime Timestamp { get; set; }
        [JsonProperty]
        public List<Transaction> Transactions { get; set; }
        [JsonProperty]
        public int Proof { get; set; }
        [JsonProperty]
        public string PreviousHash { get; set; }
    }
}