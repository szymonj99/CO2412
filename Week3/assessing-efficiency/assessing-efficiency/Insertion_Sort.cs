using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Insertion_Sort
{
    class Insertion_Sort
    {
        // Perform an in place insertion sort of passed array
        public static void InsertionSort(ref int[] input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                int currentInt = input[i];
                // Insert currentInt into sorted sequence
                int j = i - 1;
                while (j > 0 && input[j] > currentInt)
                {
                    input[j + 1] = input[j];
                    j -= 1;
                }
                input[j + 1] = currentInt;
            }
        }
    }   
}