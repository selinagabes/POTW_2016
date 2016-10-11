using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
/*
 * Gabriele, Selina
 * October 2016
 * CS Problem of the Week 1 
 * Convert unsigned integer (IP Address) to octet string
 * INPUT:int
 * OUTPUT:string
 */
namespace POTW_1
{
    class Program
    {
        static void Main(string[] args)
        {           
            int inputIPAddress = Int32.Parse(Console.ReadLine());
            string outputIP = ConvertIPToOctet(inputIPAddress);           
            Console.Write(outputIP);
            Console.ReadLine();
        }

        private static string ConvertIPToOctet(int inputIPAddress)
        {
            int[] bytesIP = new int[4];
            for (int i = 0; i < 4; i++)
            {
                bytesIP[i] = ((inputIPAddress >> (i * 8)) & 0xFF);
            }
            return (new StringBuilder(bytesIP[3] + "." + bytesIP[2] + "." + bytesIP[1] + "." + bytesIP[0]).ToString());
        }
    }
}
