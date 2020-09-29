using System;

namespace InsertionSort
{    
    class Program
    {
        // Perform an in place insertion sort of passed array
        static void InsertionSort(ref int[] input)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
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
            watch.Stop();
            Console.WriteLine("Sorting {0} elements took {1}ms.", input.Length, watch.ElapsedMilliseconds);
        }

        static int AskForNumberOfInts()
        {
            const int MAX_INTS = int.MaxValue / sizeof(int);
            Console.WriteLine("How many integers do you want in an array? 1 - {0}", MAX_INTS);
            bool bValidInput = false;            
            int iUserInput;
            do
            {
                //int.TryParse() returns false if input is not int.
                if (int.TryParse(Console.ReadLine(), out iUserInput) == false)
                {
                    Console.WriteLine("Input not recognised. Please input a number from 1 - {0}.", MAX_INTS);
                }
                else
                {
                    if (iUserInput < 1 || iUserInput > MAX_INTS)
                    {
                        Console.WriteLine("Input not recognised. Please input a number from 1 - {0}.", MAX_INTS);
                    }
                    else
                    {
                        bValidInput = true;
                    }
                }
            }
            while (!bValidInput);
            return iUserInput;
        }

        static int AskForMaxIntValue()
        {
            Console.WriteLine("What is the largest integer that you want? 1 - {0}", int.MaxValue);
            bool bValidInput = false;
            int iUserInput;
            do
            {
                //int.TryParse() returns false if input is not int.
                if (int.TryParse(Console.ReadLine(), out iUserInput) == false)
                {
                    Console.WriteLine("Input not recognised. Please input a number from 1 - {0}.", int.MaxValue);
                }
                else
                {
                    if (iUserInput < 1 || iUserInput > int.MaxValue)
                    {
                        Console.WriteLine("Input not recognised. Please input a number from 1 - {0}.", int.MaxValue);
                    }
                    else
                    {
                        bValidInput = true;
                    }
                }
            }
            while (!bValidInput);
            return iUserInput;
        }

        static void Main()
        {
            // Ask for values and create array with said values
            int NumberOfInts = AskForNumberOfInts();
            int MaxIntegerValue = AskForMaxIntValue();

            // Create array and fill array with random values
            int[] myArr = new int[NumberOfInts];
            Random random = new Random();
            for (int i = 0; i < myArr.Length; i++)
            {               
                myArr[i] = random.Next(0, MaxIntegerValue);
            }

            // Sort the array
            InsertionSort(ref myArr);

            // Output the array
            foreach (int item in myArr)
            {
                Console.Write("{0}, ", item);
            }
            Console.ReadLine();
        }
    }
}
