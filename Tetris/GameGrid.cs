/*
     
    Game grid in Tetris is 20 rows and 10 columns. I'll use 2 more rows (they will be hidden) for store block.
    The representaion of game grid look's like this:
    
                Columns
     01 02 03 04 05 06 07 08 09 10
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |         row #1  \
    -------------------------------                  = hidden rows
    |  |  |  |  |  |  |  |  |  |  |         row #2  /
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |         row #3
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |         row #5
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |         row #7
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |         row #9
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |         row #11
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |         row #13
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |         row #15
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |         row #17
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |         row #19
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |         row #21
    -------------------------------
    |  |  |  |  |  |  |  |  |  |  |
    -------------------------------
*/
using System.Data;

namespace Tetris
{
    public class GameGrid
    {
        public int rows { get; }
        public int columns { get; }

        private int[,] grid;

        public int this[int r, int c]
        { 
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        // constructor
        public GameGrid()
        {
            rows = 22;
            columns = 10;
            grid = new int[rows, columns];
        }

        public bool IsInside(int r, int c)
        {
            return r >= 0 && r < rows && c >= 0 && c < columns;
        }
        // checking cell for being empty
        public bool IsEmpty(int row, int column)
        {
            return IsInside(row, column) && grid[row, column] == 0;
        }

        // checking for row being full
        public bool CheckFullRow(int row)
        {
            for (int column = 0; column < columns; column++) 
            {
                if (grid[row, column] == 0) return false;
            }
            return true;
        }

        // checking for row being empty
        public bool CheckEmptyRow(int row)
        {
            for (int column = 0; column < columns; column++)
            {
                if (grid[row, column] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        // method for clear full row
        private void ClearRow(int row)
        {
            for (int column = 0; column < columns; column++) grid[row, column] = 0;
        }

        // method for moving rows down after clearing a full row
        private void MoveRow(int row, int number_rows)
        {
            for (int column = 0; column < columns; column++)
            {
                grid[row + number_rows, column] = grid[row, column];
                grid[row, column] = 0;
            }
        }

        // method for clear rows
        public int ClearRow()
        {
            int cleared = 0;

            for (int row = rows - 1 ; row >= 0; row--)
            {
                if (CheckFullRow(row))
                {
                    ClearRow(row);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRow(row, cleared);
                }
            }

            return cleared;
        }
    }
}