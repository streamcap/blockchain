using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockchain.Business.Model;
using Newtonsoft.Json;

// <summary>
// https://hackernoon.com/learn-blockchains-by-building-one-117428612f46
// </summary>

namespace Blockchain.Business
{
    public class Blockchain
    {
        public List<Block> Chain { get; private set; }
        private readonly List<Transaction> _tx;
        private readonly IProofHandler _proofHandler;
        private readonly IBlockchainNetworkClient _client;
        public List<string> Nodes { get; }
        public Block LastBlock => Chain.LastOrDefault();

        public Blockchain(IProofHandler proofHandler, IBlockchainNetworkClient client)
        {
            Chain = new List<Block>();
            _tx = new List<Transaction>();
            CreateBlock("1", 100);
            _proofHandler = proofHandler;
            _client = client;
            Nodes = new List<string>();
        }

        public void RegisterNode(string address)
        {
            var uri = new Uri(address);
            Nodes.Add(uri.Host);
        }

        private bool IsValidChain(List<Block> chain)
        {
            var lastBlock = chain.First();
            var i = 1;
            while (i < chain.Count)
            {
                var block = chain.ElementAt(i);
                if (block.PreviousHash != Hasher.CreateHash(lastBlock))
                {
                    return false;
                }
                if (!_proofHandler.ValidateProof(lastBlock.Proof, block.Proof))
                {
                    return false;
                }
                lastBlock = block;
                i++;
            }
            return true;
        }

        public async Task<bool> ResolveConflicts()
        {
            List<Block> longestChain = null;

            var maxLength = Chain.Count;

            foreach (var node in Nodes)
            {
                var response = await _client.GetNodeChain(node);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var chain = JsonConvert.DeserializeObject<List<Block>>(await response.Content.ReadAsStringAsync());
                if (chain.Count <= maxLength || !IsValidChain(chain))
                {
                    continue;
                }
                maxLength = chain.Count;
                longestChain = chain;
            }
            if (longestChain != null)
            {
                Chain = longestChain;
            }
            return longestChain != null;
        }

        public MiningResult MineBlock()
        {
            var proof = _proofHandler.GetProofOfWork(LastBlock.Proof);
            AddTransaction("0", "Me", 1);
            var previousHash = Hasher.CreateHash(LastBlock);
            var block = CreateBlock(previousHash, proof);

            var response = new MiningResult
            {
                Index = block.Index,
                Proof = block.Proof,
            };
            return response;
        }

        private Block CreateBlock(string previousHash, int proof)
        {
            var block = new Block
            {
                Index = Chain.Count + 1,
                Timestamp = DateTime.UtcNow,
                Transactions = _tx.ToArray(),
                Proof = proof,
                PreviousHash = previousHash ?? Hasher.CreateHash(Chain.LastOrDefault())
            };
            _tx.Clear();
            Chain.Add(block);
            return block;
        }

        public int AddTransaction(string sender, string receiver, double amount)
        {
            _tx.Add(new Transaction
            {
                Sender = sender,
                Receiver = receiver,
                Amount = amount
            });
            return LastBlock.Index + 1;
        }

    }
}
