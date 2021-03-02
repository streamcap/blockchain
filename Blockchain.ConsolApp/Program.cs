using Blockchain.Business;
using System;
using System.Linq;
using System.Net.Http;

namespace Blockchain.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");

            var proofHandler = new ProofHandler();

            var blockchain = new Business.Blockchain(proofHandler, new BlockchainNetworkClient(new HttpClient()));

            Console.WriteLine("Created new Blockchain...");

            Console.WriteLine("Mining a new block...");
            var result = blockchain.MineBlock();
            Console.WriteLine($"Mined block, result index {result.Index}, proof {result.Proof}...");

            Console.WriteLine("Adding transaction...");
            var index = blockchain.AddTransaction("Anna", "Johan", 2);
            Console.WriteLine($"Transaction added, will be in block index {index}...");
            Console.WriteLine("Mining next block...");
            result = blockchain.MineBlock();
            Console.WriteLine($"Mined block, result index {result.Index}, proof {result.Proof}...");

            Console.WriteLine("Adding transaction...");
            index = blockchain.AddTransaction("Bertil", "Johan", 3);
            Console.WriteLine($"Transaction added, will be in block index {index}...");
            Console.WriteLine("Mining next block...");
            result = blockchain.MineBlock();
            Console.WriteLine($"Mined block, result index {result.Index}, proof {result.Proof}...");

            Console.WriteLine("Adding transaction...");
            index = blockchain.AddTransaction("Caesar", "Johan", 4);
            Console.WriteLine($"Transaction added, will be in block index {index}...");
            Console.WriteLine("Mining next block...");
            result = blockchain.MineBlock();
            Console.WriteLine($"Mined block, result index {result.Index}, proof {result.Proof}...");

            Console.WriteLine("Adding transaction...");
            index = blockchain.AddTransaction("Disa", "Johan", 5);
            Console.WriteLine($"Transaction added, will be in block index {index}...");
            Console.WriteLine("Mining next block...");
            result = blockchain.MineBlock();
            Console.WriteLine($"Mined block, result index {result.Index}, proof {result.Proof}...");

            Console.WriteLine("Listing chain...");

            var previousHash = "1";

            foreach (var block in blockchain.Chain)
            {
                Console.WriteLine($"Listing block {block.Index}, proof {block.Proof}, previous hash matches? {block.PreviousHash == previousHash}");
                previousHash = Hasher.CreateHash(block);

                if (!block.Transactions.Any())
                {
                    Console.WriteLine("  No transactions in block...");
                    continue;
                }

                Console.WriteLine($"  Listing transactions for block index {block.Index}...");

                foreach (var transaction in block.Transactions)
                {
                    Console.WriteLine($"    Transaction: {transaction.Amount} from {transaction.Sender} to {transaction.Receiver}...");
                }
            }

            previousHash = "1";
            var previousProof = 100;
            foreach (var block in blockchain.Chain)
            {
                Console.WriteLine($"Proof is {block.Proof} - Valid? {proofHandler.ValidateProof(previousProof, block.Proof)}, good hash? {previousHash == block.PreviousHash}.");
                previousHash = Hasher.CreateHash(block);
                previousProof = block.Proof;
            }
        }


    }
}
