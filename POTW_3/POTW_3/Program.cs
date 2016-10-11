using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
/*
 * Gabriele, Selina 
 * October 2016
 * POTW 3 
 * Relevant Term Frequency 
 * Given a set of documents and a query term, determing the relevancy of each document
 * INPUT: N num of docs, N*string strings to represent documents, T query term 
 * OUTPUT: the relevancy of each document based on a term
 */
namespace POTW_3
{
    class Program
    {
        static void Main(string[] args)
        {
            double numOfDocs = Int32.Parse(Console.ReadLine());                                     //How many documents are we searching
            string[] docContent = new string[(int)numOfDocs];                                       
                    
            for(int i = 0; i < numOfDocs; i++)
            {
                docContent[i] = Console.ReadLine();                                                 //Get the document content
            }
            string queryTerm = Console.ReadLine();
            double docsWithTerm = docContent.Where(x => x.Contains(queryTerm)).Count();             //How many documents contain the query
            float totalTermFrequency = !docsWithTerm.Equals(0F)?(float)Math.Log10(numOfDocs / docsWithTerm): 0;                 //idf = Log10(numofDocs/docswithTerm);

            float[] docScores = new float[(int)numOfDocs];
           
            for(int j = 0; j < numOfDocs; j++)
            {
                docScores[j] = totalTermFrequency * TermFrequency(queryTerm, docContent[j]);        //tf * idf
            }
            for(int m = 0; m < numOfDocs; m++)
            {
                Console.WriteLine("{0} {1}", m + 1, docScores[m]);
            }
            Console.ReadLine(); 
        }

        private static float TermFrequency(string queryTerm, string document)
        {
           
            return new Regex(queryTerm).Matches(document).Count;                                     //Find all matches in the document 
           
        }
    }
}
