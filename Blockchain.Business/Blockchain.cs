using System;
using System.Collections.Generic;
using System.Linq;

namespace Blockchain.Business
{
    /// <summary>
    /// https://hackernoon.com/learn-blockchains-by-building-one-117428612f46
    /// </summary>

    public class Blockchain
    {
        public List<Block> Chain { get; }
        private readonly List<Transaction> _tx;
        private readonly IProofer proofer;

        public Blockchain(IProofer proofer)
        {
            Chain = new List<Block>();
            _tx = new List<Transaction>();
            CreateBlock("1", 100);
            this.proofer = proofer;
        }

        public MiningResult MineBlock()
        {
            var proof = proofer.GetProofOfWork(LastBlock.Proof);
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

        public Block LastBlock => Chain.LastOrDefault();
    }
}
