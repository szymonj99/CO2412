using System;
using System.Collections.Generic;

namespace decisions_and_primes
{
    class Program
    {
        static bool isEven(int n)
        {
            return n % 2 == 0;
        }
        
        static bool isPrime(int n)
        {
            if (n <= 1)
            {
                return false;
            }

            for (int i = 2; i <= n / 2; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        static void productOfPrimes(int n)
        {
            if (n <= 0)
            {
                Console.WriteLine("Non-positive value input.");
                return;
            }
            if (isPrime(n))
            {
                Console.WriteLine("{0} is prime.", n);
                return;
            }
            if (n == 1)
            {
                Console.WriteLine("{0} is not a big enough number.", n);
                return;
            }

            List<int> primes = new List<int>();

            for (int i = 2; i <= n / 2; i++)
            {
                if (isPrime(i))
                {
                    primes.Add(i);
                }
            }

            // Start from the number n
            int temp = n;

            List<int> outcome = new List<int>();

            while (temp > 2)
            {
                foreach (var prime in primes)
                {
                    if (temp % prime == 0)
                    {
                        outcome.Add(prime);
                        temp /= prime;
                    }
                }
            }

            foreach (var prime in outcome)
            {
                Console.Write("{0} x ", prime);
            }
        }

        static void Main(string[] args)
        {
            int n = 1;
            if (isEven(n))
            {
                Console.WriteLine("{0} is even.", n);
            }
            else
            {
                Console.WriteLine("{0} is not even.", n);
            }

            int j = 1;
            if (isPrime(j))
            {
                Console.WriteLine("{0} is prime.", j);
            }
            else
            {
                Console.WriteLine("{0} is not prime.", j);
            }

            productOfPrimes(21);

            Console.WriteLine("Program finished.");
            Console.ReadLine();
        }
    }
}
