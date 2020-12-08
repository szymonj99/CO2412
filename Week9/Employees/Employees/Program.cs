using System;
using System.IO;
using System.Collections.Generic;
using PriorityQueue;

namespace Employees
{
    public class Employee : IComparable<Employee>
    {
        public String Name { get; set; }
        public Int32 Year { get; set; }
        public Char Gender { get; set; }

        public Int32 CompareTo(Employee other)
        {
            // Lowest year has priority
            if (this.Year > other.Year)
            {
                return 1;
            }
            else if (other.Year > this.Year)
            {
                return -1;
            }
            return 0;
        }

        Employee() { }
        public Employee(String name, Int32 year, Char gender)
        {
            Name = name;
            Year = year;
            Gender = gender;
        }

        public override String ToString()
        {
            return String.Format("{0} {1} {2}", Name, Year, Gender);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue<Employee> priorityQueue = new PriorityQueue<Employee>();
            // Read file input
            const String sInputFile = "employees.txt";

            try
            {
                String[] sInput = System.IO.File.ReadAllLines(sInputFile);
                List<Employee> employees = new List<Employee>();
                foreach (String line in sInput)
                {
                    var elements = line.Trim().Split(',');
                    Employee emp = new Employee(elements[0], Int32.Parse(elements[1]), Char.Parse(elements[2]));
                    priorityQueue.Enqueue(emp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e.Message);
            }

            Int32 count = priorityQueue.Count();
            for (Int32 i = 0; i < count; i++)
            {
                Console.WriteLine(priorityQueue.Dequeue());
            }

            Console.WriteLine("Program finished.");
            Console.ReadLine();
        }
    }
}
