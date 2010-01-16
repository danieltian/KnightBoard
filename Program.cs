using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightBoard
{
    class Program
    {
        private static Stack<ValidMoveSpot> _solutionMoves = new Stack<ValidMoveSpot>();
        private static int maxX = 6;
        private static int maxY = 6;

        static void Main(string[] args)
        {
            var takenPositions = new bool[maxY, maxX];

            var solutionFound = FindSolution(takenPositions, 0, 0);

            if(solutionFound)
            {
                takenPositions = new bool[maxY, maxX];

                foreach(var m in _solutionMoves.Reverse())
                {
                    Console.Clear();
                    DrawBoard(m, takenPositions);
                    takenPositions[m.Y, m.X] = true;
                    Console.ReadLine();
                }
            }
        }

        public static void DrawBoard(ValidMoveSpot m, bool[,] takenPositions)
        {
            for(int i = 0; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    if (j == m.X && i == m.Y)
                    {
                        Console.Write("K ");
                    }
                    else if (takenPositions[i, j])
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }

                Console.WriteLine();
            }
        }

        public static bool FindSolution(bool[,] takenPositions, int x, int y)
        {
            takenPositions[y, x] = true;
            _solutionMoves.Push(new ValidMoveSpot { X = x, Y = y });

            if(_solutionMoves.Count == maxX * maxY)
            {
                return true;
            }

            foreach(var validMove in FindValidMoves(takenPositions, x, y))
            {
                var isFilled = FindSolution(takenPositions, validMove.X, validMove.Y);

                if (isFilled)
                {
                    return true;
                }
            }

            takenPositions[y, x] = false;
            _solutionMoves.Pop();
            return false;
        }

        public static List<ValidMoveSpot> FindValidMoves(bool[,] takenPositions, int x, int y)
        {
            var validMoves = new List<ValidMoveSpot>();

            if (x - 2 >= 0 && y - 1 >= 0 && !takenPositions[y - 1, x - 2])
            {
                validMoves.Add(new ValidMoveSpot {X = x - 2, Y = y - 1});
            }
            if (x - 1 >= 0 && y - 2 >= 0 && !takenPositions[y - 2, x - 1])
            {
                validMoves.Add(new ValidMoveSpot {X = x - 1, Y = y - 2});
            }
            if (x + 1 < maxX && y - 2 >= 0 && !takenPositions[y - 2, x + 1])
            {
                validMoves.Add(new ValidMoveSpot {X = x + 1, Y = y - 2});
            }
            if (x + 2 < maxX && y - 1 >= 0 && !takenPositions[y - 1, x + 2])
            {
                validMoves.Add(new ValidMoveSpot {X = x + 2, Y = y - 1});
            }
            if (x + 2 < maxX && y + 1 < maxY && !takenPositions[y + 1, x + 2])
            {
                validMoves.Add(new ValidMoveSpot {X = x + 2, Y = y + 1});
            }
            if (x + 1 < maxX && y + 2 < maxY && !takenPositions[y + 2, x + 1])
            {
                validMoves.Add(new ValidMoveSpot {X = x + 1, Y = y + 2});
            }
            if (x - 1 >= 0 && y + 2 < maxY && !takenPositions[y + 2, x - 1])
            {
                validMoves.Add(new ValidMoveSpot {X = x - 1, Y = y + 2});
            }
            if (x - 2 >= 0 && y + 1 < maxY && !takenPositions[y + 1, x - 2])
            {
                validMoves.Add(new ValidMoveSpot {X = x - 2, Y = y + 1});
            }

            return validMoves;
        }

        public struct ValidMoveSpot
        {
            public int X;
            public int Y;
        }
    }
}
