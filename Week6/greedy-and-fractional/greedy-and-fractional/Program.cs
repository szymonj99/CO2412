using System;
using System.Collections.Generic;

namespace greedy_and_fractional
{
    class Program
    {
        class Item
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public int Value { get; set; }

            public double UnitValue { get; set; }

            public Item(string name, int weight, int value)
            {
                Name = name;
                Weight = weight;
                Value = value;
                calculateUnitValue();
            }
            public override string ToString()
            {
                return String.Format(
                    "{0}\n" +
                    "Weight: {1}\n" +
                    "Value: {2}\n" +
                    "Profit: {3:0.00}", Name, Weight, Value, UnitValue);
            }

            private void calculateUnitValue()
            {
                UnitValue = Value / (double)Weight;
            }
        }

        static readonly string sInputFile = @"ItemList.txt";
        static readonly int iKnapsackCapacity = 50;

        static List<Item> ReadItemsFromFile()
        {
            List<Item> items = new List<Item>();
            
            if (!System.IO.File.Exists(sInputFile))
            {
                // return empty list if file not found.
                return new List<Item>();
            }
            string[] input = System.IO.File.ReadAllLines(sInputFile);

            foreach (string line in input)
            {
                var elements = line.Split(',');
                Item item = new Item(elements[0], int.Parse(elements[1]), int.Parse(elements[2]));
                items.Add(item);
            }

            return items;
        }

        static void Main(string[] args)
        {
            List<Item> items = ReadItemsFromFile();

            // Sort the List
            items.Sort(
                delegate (Item item1, Item item2)
                {
                    return item2.UnitValue.CompareTo(item1.UnitValue);
                });

            foreach (Item item in items)
            {
                Console.WriteLine(item);
            }

            int KnapsackWeight = 0;
            List<Item> Knapsack = new List<Item>();

            foreach(Item item in items)
            {
                if (KnapsackWeight < (iKnapsackCapacity - item.Weight))
                {
                    Knapsack.Add(item);
                    KnapsackWeight += item.Weight;
                }
                else
                {
                    Item partialItem = new Item(item.Name, iKnapsackCapacity - KnapsackWeight, (int)(item.Value / (double)(iKnapsackCapacity - KnapsackWeight)));
                    KnapsackWeight += iKnapsackCapacity - KnapsackWeight;
                }
            }

            Console.WriteLine(@"Outputting Knapsack Contents.");

            foreach (Item item in Knapsack)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Knapsack items: {0}\n" +
                "Knapsack Weight: {1}", Knapsack.Count, KnapsackWeight);

            Console.ReadLine();
        }
    }
}
