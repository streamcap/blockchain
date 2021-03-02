using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Blockchain.Business
{
    /// <summary>
    /// https://hackernoon.com/learn-blockchains-by-building-one-117428612f46
    /// </summary>

    public class Blockchain
    {
        public List<Block> Chain { get; }
        private readonly List<Transaction> _tx;

        public Blockchain()
        {
            Chain = new List<Block>();
            _tx = new List<Transaction>();
            CreateBlock("1", 100);
        }

        public MineResult MineBlock()
        {
            var proof = GetProofOfWork(LastBlock.Proof);
            AddTransaction("0", "Me", 1);
            var previousHash = CreateHash(LastBlock);
            var block = CreateBlock(previousHash, proof);

            var response = new MineResult
            {
                Index = block.Index,
                Proof = block.Proof,
            };
            return response;
        }

        public Block CreateBlock(string previousHash, int proof)
        {
            var block = new Block
            {
                Index = Chain.Count + 1,
                Timestamp = DateTime.UtcNow,
                Transactions = _tx.ToArray(),
                Proof = proof,
                PreviousHash = previousHash ?? CreateHash(Chain.LastOrDefault())
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

        public string CreateHash(Block block)
        {
            var json = JsonConvert.SerializeObject(block);
            var bytes = Encoding.UTF8.GetBytes(json);
            var hash = SHA256.Create().ComputeHash(bytes);
            return GetHexString(hash);
        }

        public int GetProofOfWork(int lastProof)
        {
            var proof = 0;
            while (!ValidateProof(lastProof, proof))
            {
                proof += 1;
            }
            return proof;
        }

        public bool ValidateProof(int lastproof, int proof)
        {
            var guess = Encoding.UTF8.GetBytes($"{lastproof}{proof}");
            var guessHash = SHA256.Create().ComputeHash(guess);
            var hashLine = GetHexString(guessHash);
            var result = hashLine.Contains("0000");
            return result;
        }

        private string GetHexString(byte[] bytes)
        {
            return bytes.Select(b => $"{b:X}").Aggregate("", (a, b) => a + b);
        }


        public Block LastBlock => Chain.LastOrDefault();
    }
}
