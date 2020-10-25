using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Knapsack01
{
    class Item
    {
        public int Weight { get; set; }
        public int Value { get; set; }
        public String Name { get; set; }

        public Item(string name, int weight, int value)
        {
            Name = name;
            Weight = weight;
            Value = value;
        }

        public override string ToString()
        {
            return "Name: " + Name + "\tWeight: " + Weight + "\tValue: " + Value;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> items = new List<Item>();
            BitVector32 vector = new BitVector32(0);

            const Int32 KnapsackCapacity = 15;

            Item compass = new Item("Compass", 3, 4);
            items.Add(compass);

            Item map = new Item("Map", 4, 4);
            items.Add(map);

            Item drink = new Item("Drink", 5, 7);
            items.Add(drink);

            Item mintCake = new Item("Kendal Mint Cake", 2, 4);
            items.Add(mintCake);

            Item phone = new Item("Phone", 3, 5);
            items.Add(phone);

            Item flashlight = new Item("Flashlight", 3, 2);
            items.Add(flashlight);

            foreach (Item item in items)
            {
                Console.WriteLine(item);
            }

            Int32 SolutionSpace = (int)Math.Pow(2, items.Count) - 1;
            BitVector32.Section bits = BitVector32.CreateSection((short)SolutionSpace);

            Int32 CurrentBestValue = 0;
            Int32 BestEnumeration = 0;
            Int32 BestEnumerationWeight = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (Int32 i = 1; i <= SolutionSpace; i++)
            {
                vector[bits] = i;
                string sVector = vector.ToString();
                sVector = sVector.Substring(sVector.Length - (items.Count + 1), items.Count);

                Int32 weight = 0;
                Int32 value = 0;
                for (Int32 j = 0; j < sVector.Length; j++)
                {
                    if (sVector[j] == '1')
                    {
                        weight += items[j].Weight;
                        value += items[j].Value;
                    }                    
                }
                if (weight < KnapsackCapacity && value > CurrentBestValue)
                {
                    CurrentBestValue = value;
                    BestEnumeration = i;
                    BestEnumerationWeight = weight;
                }
                Console.WriteLine("Alternative: {0}  \tVector: {1} \tValue: {2}", i, sVector, value);
            }

            stopwatch.Stop();
            Console.WriteLine("{0} items took {1} ms", items.Count, stopwatch.ElapsedMilliseconds);

            Console.WriteLine("Best Enumeration: {0} \tWeight: {1} \tValue: {2} ", BestEnumeration, BestEnumerationWeight, CurrentBestValue);

            Console.ReadLine();
        }
    }
}
