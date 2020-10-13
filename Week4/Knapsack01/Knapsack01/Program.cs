using System;
using System.Collections.Generic;

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

            Item compass = new Item("Compass", 3, 4);
            items.Add(compass);

            Item map = new Item("Map", 4, 4);
            items.Add(map);

            Item drink = new Item("Drink", 5, 7);
            items.Add(drink);

            Item mintCake = new Item("Kendal Mint Cake", 2, 4);
            items.Add(mintCake);

            foreach (Item item in items)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
