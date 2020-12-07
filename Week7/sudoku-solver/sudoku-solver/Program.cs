using System;

namespace sudoku_solver
{
    class Program
    {
        class Sudoku
        {
            const int N = 9;
            const int GRID = 3;
            public int[,] Puzzle { get; set; } // puzzle instance as a 9x9 2D array

            public Sudoku() { }
            public Sudoku(int[,] puzzle)
            {
                Puzzle = puzzle;
            }

            override public string ToString()
            {
                string puzzle = "";
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        puzzle += String.Format("{0} ", Puzzle[i, j]);
                    }
                    puzzle += "\n";
                }
                return puzzle;
            }

            public bool isSafe(int row, int col, int num)
            {
                return checkRow(row, col, num) && checkCol(row, col, num) && checkGrid(row, col, num);
            }

            bool checkRow(int row, int col, int num)
            {
                // Check the row to see if the number exists 
                for (int i = 0; i < N; i++)
                {
                    if (Puzzle[row, i] == num)
                    {
                        return false;
                    }
                }
                return true;
            }
            
            bool checkCol(int row, int col, int num)
            {
                // Check the column to see if the number exists
                for (int i = 0; i < N; i++)
                {
                    if (Puzzle[i, col] == num)
                    {
                        return false;
                    }
                }
                return true;
            }

            bool checkGrid(int row, int col, int num)
            {
                // Check the 3x3 grid to see if the number exists
                int startRow = row - row % GRID;
                int startCol = col - col % GRID;

                for (int i = 0; i < GRID; i++)
                {
                    for (int j = 0; j < GRID; j++)
                    {
                        if (Puzzle[(i + startRow), (j + startCol)] == num)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        static bool solveSudoku(int row, int col, int[,] instance)
        {
            int n = instance.GetLength(0);
            Sudoku puzzle = new Sudoku(instance);

            // base case we've finished the puzzle
            if (row == n - 1 && col == n)
            {
                return true;
            }
            
            // final column reached start the next row
            if (col == n)
            {
                row++;
                col = 0;
            }

            // location already has a value move onto the next column
            if (puzzle.Puzzle[row, col] > 0)
            {
                return solveSudoku(row, col + 1, puzzle.Puzzle);
            }

            for (int num = 1; num <= n; num++)
            {
                // do the checks and assign the value
                {
                    if (puzzle.isSafe(row, col, num))
                    {
                        puzzle.Puzzle[row, col] = num;

                        // move to the next column and repeat
                        if (solveSudoku(row, col + 1, puzzle.Puzzle))
                        {
                            return true;
                        }
                    }

                    // didn't work reset and try again
                    puzzle.Puzzle[row, col] = 0;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            int[,] puzzle =
            { 
                { 0, 6, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 5, 0, 9, 8, 0, 7 },
                { 1, 0, 0, 0, 8, 6, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 2 },
                { 0, 0, 0, 0, 3, 1, 7, 0, 0 },
                { 0, 0, 9, 4, 0, 0, 3, 0, 0 },
                { 4, 0, 3, 0, 0, 0, 0, 0, 0 },
                { 8, 0, 1, 9, 4, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 3, 5 }
            };

            Sudoku sudoku = new Sudoku(puzzle);

            if (solveSudoku(0, 0, puzzle))
            {
                //uses the overridden ToString() method
                Console.WriteLine(sudoku);
            }
            else
            {
                Console.WriteLine("no solution exists ");
            }

            Console.WriteLine("Program finished.");
            Console.ReadLine();
        }
    }
}
