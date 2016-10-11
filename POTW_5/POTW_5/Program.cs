using System;
using System.Collections.Generic;
/*
 * Gabriele, Selina 
 * October 2016
 * Implement a (modified) trie to search for malicious prefixes and ban Ips with said malicious prefixes 
 * Input: N * malicious prefixes(string), M * ips accessing the site
 * Output: wheteher ips are banned or not 
 *  --NOTE - not full implementation of Trie, modified search just searches for prefix and end of string ('$')--
 */
namespace POTW_5
{
    class Program
    {
        static void Main(string[] args)
        {
            int numOfMaliciousPrefixes = Int32.Parse(Console.ReadLine());
            Trie ipTrie = new POTW_5.Trie();
            for (int i = 0; i < numOfMaliciousPrefixes; i++)
            {
                ipTrie.Insert(Console.ReadLine());
            }

            int numOfIp = Int32.Parse(Console.ReadLine());
            List<string> outputString = new List<String>();
            string ip;
            for (int j = 0; j < numOfIp; j++)
            {
                ip = Console.ReadLine();
                if (ipTrie.SearchForMaliciousPrefix(ip))
                {
                    outputString.Add("banned");
                }
                else
                {
                    outputString.Add("valid");
                }
            }
            for (int k = 0; k < numOfIp; k++)
            {
                Console.WriteLine(outputString[k]);
            }
            Console.ReadLine();
        }
    }
}
