using System;
using System.Collections.Generic;
using System.Linq;

namespace bfs_and_dfs
{
    class Program
    {
        const int LANCASTER = 0;
        const int BLACKPOOL = 1;
        const int PRESTON = 2;
        const int BLACKBURN = 3;
        const int BURNLEY = 4;
        const int CHORLEY = 5;
        const int ROCHDALE = 6;
        const int SOUTHPORT = 7;
        const int BOLTON = 8;
        const int WIGAN = 9;


        static void Main(string[] args)
        {
            string[] towns = {"Lancaster", "Blackpool", "Preston", "Blackburn", "Burnley", "Chorley",
                              "Rochdale", "Southport", "Bolton", "Wigan"};

            List<Node> Towns = new List<Node>(); //List of all Nodes
            List<Node> Children = new List<Node>(); //List of children

            foreach (var t in towns) //create the nodes
            {
                Node n = new Node(t);
                Towns.Add(n);
            }

            int[,] adjacencyMatrix =
            { // 0,1,2,3,4,5,6,7,8,9
                {0,0,1,0,0,0,0,0,0,0}, //Lancaster 0
                {0,0,1,0,0,0,0,0,0,0}, //Blackpool 1
                {1,1,0,1,0,1,0,1,1,0}, //Preston 2
                {0,0,1,0,1,0,1,0,1,0}, //Blackburn 3
                {0,0,0,1,0,0,0,0,0,0}, //Burnley 4 
                {0,0,1,0,0,0,0,1,1,1}, //Chorley 5
                {0,0,0,1,0,0,0,0,1,0}, //Rochdale 6
                {0,0,1,0,0,1,0,0,0,1}, //Southport 7 
                {0,0,1,1,0,1,1,0,0,1}, //Bolton 8
                {0,0,0,0,0,1,0,1,1,0} //Wigan 9
            };

            //BuildGraph(Children, Towns); //Build the graph
            BuildAdjacency(Children, Towns, adjacencyMatrix);
            CheckGraph(Children, Towns); //Print the full graph
            Node Liverpool = new Node("Liverpool"); //Not in the graph
            var Start = Towns[ROCHDALE];
            var Destination = Towns[LANCASTER];
            Bfs bfs = new Bfs(Start, Destination);

            if (bfs.Search() == true)
            {
                Console.WriteLine("Path Found from {0} to {1}", Start, Destination);
                Stack<Node> path = bfs.Path();
                foreach (var place in path)
                {
                    Console.Write("{0} ", place);
                }
            }
            else
            {
                Console.WriteLine("Failed to Find Path");
            }                
        }

        static void BuildAdjacency(List<Node> Children, List<Node> Towns, int[,] adjacency)
        {
            for (int i = 0; i < adjacency.GetLength(0); i++)
            {
                for (int j = 0; j < adjacency.GetLength(1); j++)
                {
                    if (adjacency[i, j] == 1)
                    {
                        Children.Add(Towns[j]);

                    }

                }
                Towns[i].AddChildren(Children);
                Children.Clear();
            }
        }

        static void BuildGraph(List<Node> Children, List<Node> Towns)
        {
            //Lancaster and Blackpool
            Children.Add(Towns[PRESTON]);
            Towns[LANCASTER].AddChildren(Children);
            Towns[BLACKPOOL].AddChildren(Children);
            Children.Clear();

            //Preston
            Children.Add(Towns[SOUTHPORT]);
            Children.Add(Towns[CHORLEY]);
            Children.Add(Towns[BOLTON]);
            Children.Add(Towns[BLACKBURN]);
            Children.Add(Towns[BLACKPOOL]);
            Children.Add(Towns[LANCASTER]);
            Towns[PRESTON].AddChildren(Children);
            Children.Clear();

            //Blackburn
            Children.Add(Towns[PRESTON]);
            Children.Add(Towns[BURNLEY]);
            Children.Add(Towns[ROCHDALE]);
            Children.Add(Towns[BOLTON]);
            Towns[BLACKBURN].AddChildren(Children);
            Children.Clear();

            //Burnley
            Children.Add(Towns[BLACKBURN]);
            Towns[BURNLEY].AddChildren(Children);
            Children.Clear();

            //Chorley
            Children.Add(Towns[PRESTON]);
            Children.Add(Towns[BOLTON]);
            Children.Add(Towns[SOUTHPORT]);
            Children.Add(Towns[WIGAN]);
            Towns[CHORLEY].AddChildren(Children);
            Children.Clear();

            //Rochdale
            Children.Add(Towns[BLACKBURN]);
            Children.Add(Towns[BOLTON]);
            Towns[ROCHDALE].AddChildren(Children);
            Children.Clear();

            //Southport
            Children.Add(Towns[WIGAN]);
            Children.Add(Towns[CHORLEY]);
            Children.Add(Towns[PRESTON]);
            Towns[SOUTHPORT].AddChildren(Children);
            Children.Clear();

            //Bolton
            Children.Add(Towns[PRESTON]);
            Children.Add(Towns[WIGAN]);
            Children.Add(Towns[CHORLEY]);
            Children.Add(Towns[BLACKBURN]);
            Children.Add(Towns[ROCHDALE]);
            Towns[BOLTON].AddChildren(Children);
            Children.Clear();

            //Wigan
            Children.Add(Towns[CHORLEY]);
            Children.Add(Towns[SOUTHPORT]);
            Children.Add(Towns[BOLTON]);
            Towns[WIGAN].AddChildren(Children);
            Children.Clear();

        }

        static void CheckGraph(List<Node> Children, List<Node> Towns)
        {
            foreach (var t in Towns)
            {
                Console.Write("{0,-15} =>", t);
                foreach (var c in t.Children)
                {
                    Console.Write(" {0,-15}", c);
                }
                Console.WriteLine();
            }
        }
    }


    class Node
    {
        public string Name { get; set; }
        public List<Node> Children { get; set; }
        public Node Parent { get; set; }


        public Node(string name)
        {
            Name = name;
            Children = new List<Node>();
            Parent = null;
        }

        public void AddChildren(List<Node> children)
        {
            foreach (var c in children)
            {
                Children.Add(c);
            }
            Children = Children.OrderBy(o => o.Name).ToList();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    class Bfs
    {
        Node StartState { get; set; }
        Node GoalState { get; set; }

        Queue<Node> Fringe { get; set; }
        List<Node> Explored { get; set; }

        public Bfs(Node startState, Node goalState)
        {
            StartState = startState;
            GoalState = goalState;
            Fringe = new Queue<Node>();
            Fringe.Enqueue(StartState);
            Explored = new List<Node>();
        }

        public bool Search()
        {
            while (Fringe.Count > 0)
            {
                Node state = Fringe.Dequeue();
                Explored.Add(state);
                if (state.Name == GoalState.Name)
                    return true;

                foreach (var child in state.Children)
                {
                    bool explored = false;
                    foreach (var f in Fringe)
                    {
                        if (child.Name == f.Name)
                        {
                            explored = true;
                            break;
                        }
                    }
                    if (!explored)
                    {
                        foreach (var e in Explored)
                        {
                            if (child.Name == e.Name)
                            {
                                explored = true;
                                break;
                            }

                        }
                    }
                    if (!explored)
                    {
                        child.Parent = state;
                        Fringe.Enqueue(child);
                    }
                }
            }
            return false;
        }

        public Stack<Node> Path()
        {
            Stack<Node> Path = new Stack<Node>();
            Node path = GoalState;
            while (path.Parent != null)
            {
                Path.Push(path);
                path = path.Parent;
            }
            Path.Push(StartState);

            return Path;

        }

    }
}
