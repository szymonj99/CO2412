using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace linked_lists
{
    class Program
    {
        class Student
        {
            public string Name { get; set; }
            public string ID { get; set; }

            public Student(string name, string id)
            {
                Name = name; ID = id;
            }

            public override string ToString()
            {
                return String.Format("{0} Student ID: {1}", Name, ID);
            }

            static void Main(string[] args)
            {
                LinkedList<Student> students = new LinkedList<Student>();
                string name = ""; // Student name;
                string id = ""; // Student id;
                                // build instance and add to the list
                using (var reader = new StreamReader(@"students.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        name = values[0];
                        id = values[1];

                        Student student = new Student(name, id);
                        students.AddLast(student);
                    }
                }

                foreach (var student in students)
                {
                    Console.WriteLine(student);
                }

                Console.WriteLine("Total students: {0}", students.Count);

                // Remove a single node "Elroy Erben" from the list
                var current = students.First;
                while (current != null)
                {
                    string sName = current.Value.Name.Trim().ToLower();
                    if (sName == "elroy erben")
                    {
                        var nodeToDelete = current;
                        Console.WriteLine("Removing {0}", current.Value.Name);
                        current = current.Next;

                        students.Remove(nodeToDelete);
                        break;
                    }
                    else
                    {
                        current = current.Next;
                    }
                }
                Console.WriteLine("Total Students: {0}", students.Count);
            }
        }
    }
}
