using System;
using System.IO;
using System.Collections.Generic;
using Merge_Sort;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Linq;

namespace randomWrite
{
    class Program
    {
        static Int32 ITERATIONS = 12; // How many times to run each algorithm.
        static Int32 MIN = 1;
        static Int32 MAX = 50;
        static Int32 FACTOR = 3; // Each iteration the number of elements in list/array will increase by
        static string PATH = @"efficiency-output.txt"; // Using @ sign to take the string literally.

        static void Main(string[] args)
        {
            string output = "";
            Random random = new Random();

            // Insertion Sort
            output += "Insertion Sort\n";
            Console.WriteLine(@"Insertion Sort");
            // For each iteration
            for (Int32 i = 0; i < ITERATIONS; i++)
            {
                // Create list/array of factorial elements
                List<int> unsorted = new List<int>();
                for (Int32 j = 0; j < Math.Pow(FACTOR, i); j++)
                {
                    unsorted.Add(random.Next(MIN, MAX + 1));
                }
                // Fill list/array with random numbers.
                Stopwatch stopwatch = new Stopwatch();
                int[] unsortedArray = unsorted.ToArray();
                stopwatch.Start();

                // Sort here
                Insertion_Sort.Insertion_Sort.InsertionSort(ref unsortedArray);

                stopwatch.Stop();
                output += "RunTime: " + stopwatch.ElapsedMilliseconds + " ms Elements " + unsorted.Count + "\n";
                Console.WriteLine(@"RunTime: {0} ms Elements {1}", stopwatch.ElapsedMilliseconds, unsorted.Count);
            }

            output += "\n";
            Console.WriteLine(@"");

            // Merge Sort
            output += "Merge Sort\n";
            Console.WriteLine(@"Merge Sort");
            // For each iteration
            for (Int32 i = 0; i < ITERATIONS; i++)
            {
                // Create list/array of factorial elements
                List<int> unsorted = new List<int>();
                for (Int32 j = 0; j < Math.Pow(FACTOR, i); j++)
                {
                    unsorted.Add(random.Next(MIN, MAX + 1));
                }

                // Fill list/array with random numbers.
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                // Sort here
                Merge_Sort.Merge_Sort.MergeSort(ref unsorted);

                stopwatch.Stop();
                output += "RunTime: " + stopwatch.ElapsedMilliseconds + " ms Elements " + unsorted.Count + "\n";
                Console.WriteLine(@"RunTime: {0} ms Elements {1}", stopwatch.ElapsedMilliseconds, unsorted.Count);
            }

            Console.WriteLine(@"Comparison done. Saving File.");
            File.WriteAllText(PATH, output);
            Console.WriteLine(@"File saved. enter any key to exit.");
            Console.ReadLine();
        }
    }
}