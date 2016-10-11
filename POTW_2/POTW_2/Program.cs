using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
/*
 * Gabriele, Selina 
 * October 2016
 * Given an X and Y Coordinate, find K closest-coordinates out of N inputs
 * Input: X and Y value, K number of points, N>=K total points, N coordinates
 * Output: List of K closest coordinates
 */
namespace POTW_2
{

    class Program
    {
        public static Point willLocation { get; set; }
        static void Main(string[] args)
        {
            int womenIndex=0;
            string[] coordinates = Console.ReadLine().Split();                                           //Get Will's Location
            willLocation = new Point(Int32.Parse(coordinates[0]),
                                            Int32.Parse(coordinates[1]));
            int interestedWomen = Int32.Parse(Console.ReadLine());                                       //How many lady's he digging
            int totalWomen = Int32.Parse(Console.ReadLine());                                            //How many are around
            Point[] womenLocations = new Point[totalWomen];
            Point[] closestWomen = new Point[interestedWomen];                                           //for output
            for (int i = 0; i < totalWomen; i++)
            {
                coordinates = Console.ReadLine().Split();                                                //Where dey at doe
                womenLocations[i] = new Point(Int32.Parse(coordinates[0]),
                                                Int32.Parse(coordinates[1]));
            }
            Point kthSmallest = Select(womenLocations, 0, womenLocations.Length - 1, interestedWomen);  //Pick the kth closest one
            double kthDistance = GetDistance(kthSmallest);                                              //get her distance
            Console.WriteLine();
            for(int j = 0; j < totalWomen; j++)
            {
                if(GetDistance(womenLocations[j]) < kthDistance)
                {
                    closestWomen[womenIndex] = womenLocations[j];                                        //put the rest of the women who are closer in an array    
                    
                    womenIndex++;
                }
            }
            QuickSort(closestWomen, 0, interestedWomen-1);
            for(int m = 0; m < interestedWomen; m++)
            {
                Console.WriteLine("{0} {1}", closestWomen[m].X.ToString(), closestWomen[m].Y.ToString());   //Let will know their location 
            }
            Console.ReadLine();
       
        }

       
        //split the coordinates into smaller and larger portion in reference to the kth element
        //return the index that the kth smallest element is at
        public static int Partition(int pivotIndex, int leftIndex, int rightIndex, Point[] coordinates)
        {          
            //put the pivot at the end of the array 
            Swap(coordinates, pivotIndex, rightIndex);
            int store = leftIndex;
            for(int i=leftIndex; i <rightIndex; i++)
            {
                if(GetDistance(coordinates[i]) < GetDistance(coordinates[rightIndex]))
                {
                    Swap(coordinates, store, i);
                    store++;
                }
            }
            //restore pivot
            Swap(coordinates, store, rightIndex);
            return store;
        }
        //find the point of the kth element and return it
        public static Point Select(Point[] coordinates, int leftIndex, int rightIndex, int k)
        {
            if (leftIndex == rightIndex)
                return coordinates[leftIndex];
            int pivotIndex = new Random().Next(leftIndex, rightIndex);
            pivotIndex = Partition(pivotIndex, leftIndex, rightIndex, coordinates);

            if (k == pivotIndex)
                return coordinates[k];
            else if (k < pivotIndex)
                return Select(coordinates, leftIndex, pivotIndex - 1, k);
            else
                return Select(coordinates, pivotIndex + 1, rightIndex, k);
        }
        //Sort the array for output using QuickSort
        private static void QuickSort(Point[] coordinates, int leftIndex, int rightIndex)
        {
            if (leftIndex >= rightIndex)
                return;
            Point pivotPoint = coordinates[leftIndex];
            int leftStore = leftIndex;
            int rightStore = rightIndex;
            while(leftStore < rightStore)
            {
                while(leftStore < rightStore && GetDistance(coordinates[rightStore]) > GetDistance(pivotPoint))
                {
                    rightStore--;
                }
                coordinates[leftStore] = coordinates[rightStore];
                while(leftStore < rightStore && GetDistance(coordinates[leftStore]) < GetDistance(pivotPoint))
                {
                    leftStore++;
                }
                coordinates[rightStore] = coordinates[leftStore];
            }
            coordinates[leftStore] = pivotPoint;

            QuickSort(coordinates, leftIndex, leftStore - 1);
            QuickSort(coordinates, leftStore + 1, rightIndex);
        }

        //Calculate the distance between will's location and the lady's
        public static double GetDistance(Point neighbour)
        {
           double dist = Math.Sqrt(Math.Pow(neighbour.X - willLocation.X, 2) + Math.Pow(neighbour.Y - willLocation.Y, 2));

            return dist;
        }

       //Swap for partitioning
        private static void Swap(Point[] coordinates, int i, int j)
        {
            Point tempSwap = coordinates[i];
            coordinates[i] = coordinates[j];
            coordinates[j] = tempSwap;
              
        }

 
    }
}
