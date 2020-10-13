using System;

namespace Knapsack01
{
    class Item
    {
        public int Weight { get; set; }
        public int Value { get; set; }
        public String Name { get; set; }

        public Item(ref string name, int weight, int value)
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
        }
    }
}
