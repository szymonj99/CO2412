// Szymon Janusz 20792986

using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Assignment1
{
    public class Chocolate
    {
        Chocolate() { }
        public Chocolate(string name, decimal cost, decimal value)
        {
            Name = name;
            Cost = cost;
            Value = value;
        }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal Value { get; set; }
        public decimal Profit => Value - Cost;

        public override string ToString()
        {
            return String.Format("Name: {0,-30}\tCost: £{1:0.00}\tValue: £{2:0.00}\tProfit: £{3:0.00}",
                Name, Cost, Value, Profit);
        }
    }

    public class GiftBox
    {
        public List<Chocolate> Chocolates { get; set; }
        public decimal ProductionCost { get; set; }
        public decimal RetailValue { get; set; }
        public decimal Profit => RetailValue - ProductionCost;
        public override string ToString()
        {
            return String.Format("Number of Chocolates: {0}\tProduction Cost: £{1:0.00}\tRetail Value: £{2:0.00}\tProfit: £{3:0.00}",
                Chocolates.Count, ProductionCost, RetailValue, Profit);
        }

        public void PrintChocolates()
        {
            foreach (var chocolate in Chocolates)
            {
                Console.WriteLine(chocolate);
            }
        }

        public void SaveToFile(string fileName)
        {
            string toSave = "";
            int index = 1;
            foreach (Chocolate chocolate in Chocolates)
            {
                toSave += String.Format("{0,-4} {1}\n", index.ToString() + ':', chocolate.Name);
                index++;
            }
            toSave += System.Environment.NewLine;
            toSave += String.Format("Cost £{0:0.00}\n", ProductionCost);
            toSave += String.Format("Retail Price £{0:0.00}\n", RetailValue);
            toSave += String.Format("Profit £{0:0.00}", Profit);
            System.IO.File.WriteAllText(fileName, toSave);
        }
    }

    class Program
    {
        // Constants

        static readonly decimal dMaximumProductionCost = 1.96M;
        static readonly int iMinimumChocolates = 14;
        static readonly string sAllChocolatesOutputFile = @"all_chocolates.txt";
        static readonly string sGiftBoxChocolateCandidatesFile = @"chocolates.txt";
        static readonly string sOptimalSolutionOutput = @"optimal_giftbox.txt";

        // Functions

        // Read a list of Chocolates from file into a list.
        static List<Chocolate> ReadFileToList()
        {
            if (!File.Exists(sGiftBoxChocolateCandidatesFile))
            {
                Console.WriteLine("ERROR: File does not exist. File: {0}", sGiftBoxChocolateCandidatesFile);
                return new List<Chocolate>();
            }

            List<Chocolate> chocolates = new List<Chocolate>();
            string[] input = File.ReadAllLines(sGiftBoxChocolateCandidatesFile);
            foreach (string line in input)
            {
                // Split the elements
                var elements = line.Split(',');
                Chocolate chocolate = new Chocolate(elements[0], Decimal.Parse(elements[1]), Decimal.Parse(elements[2]));
                chocolates.Add(chocolate);
            }

            return chocolates;
        }

        // Create all possible permutations of ingredients and processes, then save them to a file.
        static void MakingChocolates()
        {
            List<string> FirstIngredients = new List<string> { "Strawberry", "Mint", "Nougat", "Truffle", "Hazelnut", "Orange", "Toffee" };
            List<string> SecondIngredients = new List<string> { "Rosemary", "Thyme", "Sage", "Chilli", "Pepper", "Lemongrass", "Sea salt" };
            List<string> Processes = new List<string> { "Surprise", "Whip", "Delight", "Explosion", "Cream", "Crunch", "Whirl" };

            string output = "";
            int index = 1;
            // Iterate over every list and create all permutations of possible chocolates.
            foreach (string firstIngredient in FirstIngredients)
            {
                foreach (string secondIngredient in SecondIngredients)
                {
                    foreach (string process in Processes)
                    {
                        string line = String.Format("{0,-4} {1} and {2} {3}\n",
                            index.ToString() + '.', firstIngredient, secondIngredient, process);
                        output += line;
                        index++;
                    }
                }
            }

            // Remove the new line at the end of the string so that the file is n^3 lines long only.
            // More performant to do it here rather than every iteration of the n^3 loop, checking the string length.
            output = output.Substring(0, output.Length - System.Environment.NewLine.Length);

            // Save the file
            System.IO.File.WriteAllText(sAllChocolatesOutputFile, output);
        }

        // Full enumeration of the gift boxes
        static void FullEnumerationOfGiftBoxes(ref List<Chocolate> AllChocolates, ref List<GiftBox> GiftBoxes)
        {
            // How many enumerations in total the program will have to perform.
            int SolutionSpace = (int)Math.Pow(2, AllChocolates.Count) - 1;

            // Start from i = 2 ^ MinimumChocolates as all the enumerations of i below that
            // will be guaranteed to not be optimal.
            for (int i = (int)Math.Pow(2, iMinimumChocolates) - 1; i <= SolutionSpace; i++)
            {
                string bitString = Convert.ToString(i, 2);
                bitString = bitString.PadLeft(AllChocolates.Count);

                // Count how many chocolates the program will evaluate in the current iteration.
                int charOccurence = 0;
                foreach (char ch in bitString)
                {
                    if (ch == '1')
                    {
                        charOccurence++;
                    }
                }
                // Only enumerate a possible outcome if it meets the MinimumChocolates constraint.
                if (charOccurence >= iMinimumChocolates)
                {
                    List<Chocolate> chocolates = new List<Chocolate>();

                    decimal productionCost = 0M;
                    decimal retailValue = 0M;

                    for (int j = 0; j < bitString.Length; j++)
                    {
                        // '1' represents the item is taken, '0' represents the item being not taken
                        if (bitString[j] == '1')
                        {
                            chocolates.Add(AllChocolates[j]);
                            productionCost += AllChocolates[j].Cost;
                            retailValue += AllChocolates[j].Value;
                        }
                    }
                    // Only add the gift box to potential candidates for the most optimal solution
                    // if it meets production cost contraints.
                    if (productionCost <= dMaximumProductionCost)
                    {
                        GiftBox giftbox = new GiftBox
                        {
                            Chocolates = chocolates,
                            RetailValue = retailValue,
                            ProductionCost = productionCost,
                            //Profit = retailValue - productionCost
                        };
                        GiftBoxes.Add(giftbox);
                    }
                }
            }
        }

        // Iterate over all possible gift boxes and select
        // the most optimal one
        static GiftBox GetOptimalSolution(ref List<GiftBox> GiftBoxes)
        {
            GiftBox mostValued = new GiftBox();
            foreach (var giftbox in GiftBoxes)
            {
                // To get the gift box with highest profit, change
                // RetailValue to Profit
                if (giftbox.RetailValue > mostValued.RetailValue)
                {
                    mostValued = giftbox;
                }
            }
            return mostValued;
        }

        static void Main()
        {
            // Part 1 - Making Chocolates.
            // Calculate all possible permutations and save to output file.
            MakingChocolates();

            // Part 2 - Making Gift Boxes
            // Read the chocolates file to list
            List<Chocolate> allChocolates = ReadFileToList();
            // Then output the chocolates from the file to the screen.
            Console.WriteLine("Outputting All Chocolates from file.\n");
            foreach (Chocolate chocolate in allChocolates)
            {
                Console.WriteLine(chocolate);
            }

            Console.WriteLine("\nEnumerating all permutations.");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Most optimal solution = Gift Box with >= 14 chocolates, AND GiftBox.ProductionCost <= 1.96
            // List storing gift box enumerations.
            List<GiftBox> giftBoxes = new List<GiftBox>();
            // This gets all the gift box candidates that meet the constraints.
            FullEnumerationOfGiftBoxes(ref allChocolates, ref giftBoxes);

            stopwatch.Stop();
            Console.WriteLine("Full enumeration of {0} Gift Boxes took {1} ms\n",
                (int)Math.Pow(2, allChocolates.Count) - 1 - (int)Math.Pow(2, iMinimumChocolates),
                stopwatch.ElapsedMilliseconds);
            
            // The optimal solution for this problem is defined as
            // the gift box with the highest retail value
            GiftBox mostValued = GetOptimalSolution(ref giftBoxes);
            Console.WriteLine("The GiftBox with highest Retail Value is:\n" + mostValued);
            mostValued.PrintChocolates();
            mostValued.SaveToFile(sOptimalSolutionOutput);

            Console.WriteLine("\nProgram has finished executing. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}