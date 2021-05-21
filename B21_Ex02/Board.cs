using System;
using System.Text;
using System.Collections.Generic;

namespace B21_Ex02
{
    public class Board
    {
        private int m_Size;
        private char[,] m_Board;

        public Board(int size)
        {
            m_Size = size;
            m_Board = new char[size, size];
        }

        public void CopyBoard(Board i_boardtocopy)
        {
            for (int row = 1; row <= Size; row++)
            {
                for (int col = 1; col <= Size; col++)
                {
                    m_Board[row - 1, col - 1] = i_boardtocopy.m_Board[row - 1, col - 1];
                }
            }
        }

        public int Size
        {
            get
            { 
                return m_Size;
            }

            set
            {
               m_Size = value;
            }
        }

        public void SetCell(Cell i_Cell, char symbol)
        {
            m_Board[i_Cell.Row - 1, i_Cell.Col - 1] = symbol;
        }

        public char GetCell(Cell i_Cell)
        {
            return m_Board[i_Cell.Row - 1, i_Cell.Col - 1];
        }

        private bool isCellIsInsideBoard(Cell i_Cell)
        {
            bool IsCellIsInsideBoard = true;
            if (i_Cell.Row < 1 || i_Cell.Row > Size || i_Cell.Col < 1 || i_Cell.Col > Size)
            {
                IsCellIsInsideBoard = false;
            }

            return IsCellIsInsideBoard;
        }

        internal bool IsCellOk(Cell i_CellChoose)
        {
            bool validCellChoose = false;

            if (isCellIsInsideBoard(i_CellChoose) && IsEmptyCell(i_CellChoose))
            {
                validCellChoose = true;
            }

            return validCellChoose;
        }

        public bool IsEmptyCell(Cell i_Cell)
        {
            bool emptyCell = true;

            if (GetCell(i_Cell) != '\0')
            {
                emptyCell = false;
            }

            return emptyCell;
        }

        public void ClearBoard()
        {
            this.m_Board = new char[this.m_Size, this.m_Size];
        }

        //This methods calculates win/loss situation by passing Cell by Cell
        public bool CheckLoss(char i_Symbol)
        {
            bool isLoss = false;
            if (CheckRowLoss(i_Symbol) || CheckColLoss(i_Symbol) || CheckDiagonalLoss(i_Symbol))
            {
                isLoss = true;
            }

            return isLoss;
        }

        public bool CheckDiagonalLoss(char i_Symbol)
        {
            bool isDiagonal = false;
            int sameSymbolCount = 0;

            for (int rowAndCol = 1; rowAndCol <= Size; rowAndCol++)
            {
                if (m_Board[rowAndCol - 1, rowAndCol - 1] == i_Symbol)
                {
                    sameSymbolCount++;
                }
            }

            if (sameSymbolCount == Size)
            {
                isDiagonal = true;
            }
            else
            {
                int row = 3;
                for (int col = 1; col <= Size; col++)
                {
                    if (m_Board[row - 1, col - 1] == i_Symbol)
                    {
                        sameSymbolCount++;
                    }
                    row--;
                }
                if (sameSymbolCount == Size)
                {
                    isDiagonal = true;
                }
            }

            return isDiagonal;
        }

        public bool CheckRowLoss(char i_Symbol)
        {
            bool isRow = false;
            int sameSymbolCount = 0;

            for (int row = 1; row <= Size; row++)
            {
                for (int col = 1; col <= Size; col++)
                {
                    if (m_Board[row - 1, col - 1] == i_Symbol)
                    {
                        sameSymbolCount++;
                    }
                }
                if (sameSymbolCount == Size)
                {
                    isRow = true;
                    break;
                }
            }

            return isRow;
        }

        public bool CheckColLoss(char i_Symbol)
        {
            bool isCol = false;
            int sameSymbolCount = 0;

            for (int col = 1; col <= Size; col++)
            {
                for (int row = 1; row <= Size; row++)
                {
                    if (m_Board[row - 1, col - 1] == i_Symbol)
                    {
                        sameSymbolCount++;
                    }
                }
                if (sameSymbolCount == Size)
                {
                    isCol = true;
                    break;
                }
            }

            return isCol;
        }

        //This methods calculates win/loss situation only at the row,col,diagonal of the last move
        public bool CheckPlayerLoss(Cell i_LastMove, char i_symbol)
        {
            bool losss = false;

            if ((i_LastMove.Col == i_LastMove.Row) || (i_LastMove.Col + i_LastMove.Row == Size + 1))
            {
                if (isFullDiagonal(i_symbol))
                {
                    losss = true;
                }
            }

            if ((!losss) && (isFullRow(i_LastMove, i_symbol) || isFullCol(i_LastMove, i_symbol)))
            {
                losss = true;
            }

            return losss;
        }

        private bool isFullRow(Cell i_LastMove, char i_symbol)
        {
            bool isFullRow = true;

            for (int colIndex = 1; colIndex <= Size; colIndex++)
            {
                Cell currCell = new Cell(i_LastMove.Row, colIndex);
                if (GetCell(currCell) != i_symbol)
                {
                    isFullRow = false;
                    break;
                }
            }

            return isFullRow;
        }

        private bool isFullCol(Cell i_LastMove, char i_symbol)
        {
            bool isFullCol = true;
           
            for (int RowIndex = 1; RowIndex <= Size; RowIndex++)
            {
                Cell currCell = new Cell(RowIndex, i_LastMove.Col);
                if (GetCell(currCell) != i_symbol)
                {
                    isFullCol = false;
                    break;
                }
            }

            return isFullCol;
        }

        private bool isFullDiagonal(char i_symbol)
        {
            bool win = false;
            bool foundDiffCell = false;
            int rowAndColIndex = 1;

            while (rowAndColIndex <= Size)
            {
                Cell currCell = new Cell(rowAndColIndex, rowAndColIndex);
                if (GetCell(currCell) != i_symbol)
                {
                    foundDiffCell = true;
                    break;
                }

                rowAndColIndex++;
            }

            if (!foundDiffCell)
            {
                win = true;
            }

            if (!win)
            {
                foundDiffCell = false;
                int rowIndex = Size;
                int colIndex = 1;
                while ((rowIndex >= 1) && (colIndex <= Size))
                {
                    Cell currCell = new Cell(rowIndex, colIndex);
                    if (GetCell(currCell) != i_symbol)
                    {
                        foundDiffCell = true;
                        break;
                    }

                    rowIndex--;
                    colIndex++;
                }
            }

            if (!foundDiffCell)
            {
                win = true;
            }

            return win;
        }
    }

    public struct Cell
    {
        private int m_Row;
        private int m_Col;

        public Cell(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }

        public int Col
        {
            get
            {
                return m_Col;
            }

            set
            {
                m_Col = value;
            }
        }
    }
}
