using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProblemSolvingTests
{

    //Results from codewars.com Sudoko kata
    //https://www.codewars.com/kata/53db96041f1a7d32dc0004d2
    [Trait("Problem Solving", "")]
    public class SudokoTests
    {
        [Fact]
        void WhenBoardIsFinishedCorrectly_ShouldReturnFinished()
        {
            var board = new int[][]
             {
                  new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                  new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                  new int[] {1, 9, 8, 3, 4, 2, 5, 6, 7},
                  new int[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
                  new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                  new int[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
                  new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                  new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                  new int[] {3, 4, 5, 2, 8, 6, 1, 7, 9},
             };

            Assert.Equal(SuccessMessage, DoneOrNot(board));
        }

        [Fact]
        void WhenBoardHasSameNumberInABlock_ShouldReturnTryAgain()
        {
            //if counting left to right the second block of nine numbers has two '7's
            var board = new int[][]
             {
                  new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                  new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                  new int[] {1, 9, 8, 7, 4, 2, 5, 6, 3},
                  new int[] {8, 5, 9, 3, 6, 1, 4, 2, 7},
                  new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                  new int[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
                  new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                  new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                  new int[] {3, 4, 5, 2, 8, 6, 1, 7, 9},
             };

            Assert.Equal(ReTryMessage, DoneOrNot(board));
        }

        [Fact]
        void WhenBoardHasSameNumberInARow_ShouldReturnTryAgain()
        {
            var board = new int[][]
             {
                  new int[] {5, 3, 4, 6, 4, 8, 9, 1, 2},//has two '4's
                  new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                  new int[] {1, 9, 8, 3, 7, 2, 5, 6, 7},//has two '7's
                  new int[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
                  new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                  new int[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
                  new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                  new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                  new int[] {3, 4, 5, 2, 8, 6, 1, 7, 9},
             };

            Assert.Equal(ReTryMessage, DoneOrNot(board));
        }

        [Fact]
        void WhenBoardHasSameNumberInAColumn_ShouldReturnTryAgain()
        {
            //First columns has two '4's and second column has two '3's
            var board = new int[][]
             {
                  new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                  new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                  new int[] {1, 9, 8, 3, 4, 2, 5, 6, 7},
                  new int[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
                  new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                  new int[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
                  new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                  new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                  new int[] {4, 3, 5, 2, 8, 6, 1, 7, 9},
             };

            Assert.Equal(ReTryMessage, DoneOrNot(board));
        }

        private readonly static string SuccessMessage = "Finished!";
        private readonly static string ReTryMessage = "Try again!";

        public static string DoneOrNot(int[][] board)
        {
            return (CheckByRow(board) && CheckByColumn(board) && CheckByBlocks(board)) ? SuccessMessage : ReTryMessage;
        }

        private static bool CheckByRow(int[][] board)
        {
            for (int row = 0; row < 9; row++)
            {
                if (board[row].Distinct().Count() != 9)
                    return false;
            }
            return true;
        }

        private static bool CheckByColumn(int[][] board)
        {
            for (int col = 0; col < 9; col++)
            {
                var hashTable = new HashSet<int>();
                for (int row = 0; row < 9; row++)
                {
                    hashTable.Add(board[row][col]);
                }
                if (hashTable.Count() != 9)
                    return false;
            }
            return true;
        }

        private static bool CheckByBlocks(int[][] board)
        {
            int row = 0;
            int column = 0;
            int blocks = 0;
            while (blocks < 9)
            {
                var table = new HashSet<int>();
                do
                {
                    do
                    {
                        table.Add(board[row][column]);
                        column++;
                    }
                    while (column % 3 != 0);
                    row++;
                    column -= 3;
                }
                while (row % 3 != 0);

                if (table.Count() != 9)
                    return false;
                blocks++;
                if (blocks % 3 == 0)
                {
                    column += 3;
                    row = 0;
                }
            }
            return true;
        }
    }
}
