using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Blockchain.Business
{
    public class Blockchain
    {
        private readonly List<Block> _chain;
        private readonly List<Transaction> _tx;
        private readonly SHA256 _shaEngine;

        public Blockchain()
        {
            _shaEngine = SHA256.Create();
            _chain = new List<Block>();
            _tx = new List<Transaction>();
            CreateBlock("1", 100);
        }

        public Block CreateBlock(string previousHash, int proof)
        {
            var block = new Block
            {
                Index = _chain.Count + 1,
                Timestamp = DateTime.UtcNow,
                Transactions = _tx,
                Proof = proof,
                PreviousHash = previousHash ?? CreateHash(_chain.LastOrDefault())
            };
            _tx.Clear();
            _chain.Add(block);
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

        public string CreateHash(Block block)
        {
            var json = JsonConvert.SerializeObject(block);
            var bytes = Encoding.Unicode.GetBytes(json);
            var hash = _shaEngine.ComputeHash(bytes);
            return Encoding.Unicode.GetString(hash);
        }

        public int ProofOfWork(int lastProof)
        {
            var proof = 0;
            while (!ValidateProof(lastProof, proof))
            {
                proof += 1;
            }

            return proof;
        }

        private bool ValidateProof(int lastproof, int proof)
        {
            var guess = Encoding.Unicode.GetBytes($"{lastproof}{proof}");
            var guessHash = _shaEngine.ComputeHash(guess);
            return Encoding.Unicode.GetString(guessHash).StartsWith("0000");
        }


        public Block LastBlock => _chain.LastOrDefault();
    }
}
