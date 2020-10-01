using System;

namespace recursion_and_stack
{
    class Program
    {
        private static int Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }

            return n * Factorial(n - 1);
        }

        private static int NonRecursiveFactorial(int n)
        {
            if (n == 0)
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

        static void Main(string[] args)
        {
            Console.WriteLine("{0}", Factorial(5));
            Console.WriteLine("{0}", Factorial(9));
            Console.WriteLine("{0}", Factorial(0));
            Console.WriteLine("{0}", NonRecursiveFactorial(9));
            Console.ReadLine();
        }
    }
}
