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
                    "Weight: {0}\n" +
                    "Value: {1}\n" +
                    "Profit: {2:0.00}", Weight, Value, UnitValue);
            }

            private void calculateUnitValue()
            {
                UnitValue = Value / (double)Weight;
            }
        }

        static readonly string sInputFile = @"ItemList.txt";

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
            foreach(Item item in items)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
