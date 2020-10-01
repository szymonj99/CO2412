using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace binary_search
{
    class Program
    {
        // Returns the index of NUM in the array. Returns -1 if it is not found
        private static int RecursiveBinarySearch(ref int[] myArray, int NUM, int MIN, int MAX)
        {
            // The NUM is not in the array.
            if (MIN > MAX)
            {
                return -1;
            }
            
            int max = myArray.Length - 1;
            int midPoint = (MIN + MAX) / 2;

            if (myArray[midPoint] == NUM)
            {
                return midPoint;
            }
            else if (myArray[midPoint] > NUM)
            {
                return RecursiveBinarySearch(ref myArray, NUM, MIN, midPoint - 1);
            }
            else
            {
                return RecursiveBinarySearch(ref myArray, NUM, midPoint + 1, MAX);
            }
        }

        private static int IterativeBinarySearch(ref int[] myArray, int NUM)
        {
            int MIN = 0;
            int MAX = myArray.Length - 1;

            while (MIN <= MAX)
            {
                int midPoint = (MIN + MAX) / 2;
                if (NUM == myArray[midPoint])
                {
                    return midPoint;
                }
                else if (myArray[midPoint] > NUM)
                {
                    MAX = midPoint - 1;
                }
                else
                {
                    MIN = midPoint + 1;
                }
            }

            // Item not found
            return -1;
        }

        static void Main(string[] args)
        {
            int[] myArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Console.WriteLine(RecursiveBinarySearch(ref myArray, 7, 0, myArray.Length - 1));
            Console.WriteLine(IterativeBinarySearch(ref myArray, 7));

            Console.ReadLine();
        }
    }
}
