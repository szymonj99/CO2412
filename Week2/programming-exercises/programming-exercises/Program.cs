using System;

namespace programming_exercises
{
    class Program
    {
        // Non-recursive factorial
        private static int NonRecursiveFactorial(int n)
        {
            if (n <= 1)
            {
                return 1;
            }

            int output = 1;
            for (int i = 1; i <= n; i++)
            {
                output *= i;
            }
            return output;
        }

        struct MaxMinNum
        {
            public int max;
            public int min;
        }

        // Get the largest number in an array of ints
        private static MaxMinNum GetMaxMinNums(ref int[] myArray)
        {
            int maximum = int.MinValue;
            int minimum = int.MaxValue;

            for (int i = 0; i < myArray.Length; i++)
            {
                if (myArray[i] > maximum)
                {
                    maximum = myArray[i];
                }
                if (myArray[i] < minimum)
                {
                    minimum = myArray[i];
                }
            }

            MaxMinNum maxMinNum;
            maxMinNum.max = maximum;
            maxMinNum.min = minimum;

            return maxMinNum;
        }

        private static bool isPalindrome(ref string s)
        {
            if (s.Length == 1)
            {
                return true;
            }
            if (s[0] != s[s.Length - 1])
            {
                return false;
            }
            else
            {
                string substring = s.Substring(1, s.Length - 2);
                Console.WriteLine("Checking {0}", substring);
                return (isPalindrome(ref substring));
            }
        }

        static void Main(string[] args)
        {
            string racecar = "racecar";
            if (isPalindrome(ref racecar))
            {
                Console.WriteLine("{0} is a palindrome", racecar);
            }
            else
            {
                Console.WriteLine("{0} is NOT a palindrome", racecar);
            }

            string testing = "testing";
            if (isPalindrome(ref testing))
            {
                Console.WriteLine("{0} is a palindrome", testing);
            }
            else
            {
                Console.WriteLine("{0} is NOT a palindrome", testing);
            }
            Console.ReadLine();
        }
    }
}
