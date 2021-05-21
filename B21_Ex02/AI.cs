using System;

namespace B21_Ex02
{
    public class AI
    {
        public struct State
        {
            public Board m_Board;
            public Cell m_LastAddedCell;
            private Game.WhoIsPlaying m_CurrPlayer;

            public State(State i_PrevState)
            {
                m_LastAddedCell = new Cell();
                Board copyBoard = new Board(i_PrevState.m_Board.Size);
                copyBoard.CopyBoard(i_PrevState.Board);
                m_Board = copyBoard;
                m_CurrPlayer = i_PrevState.CurrPlayer;
            }

            public State(Board i_Board)
            {
                m_LastAddedCell = new Cell();
                m_Board = i_Board;
                m_CurrPlayer = Game.WhoIsPlaying.Computer;
            }

            public Cell Cell
            {
                get
                {
                    return m_LastAddedCell;
                }

                set
                {
                    m_LastAddedCell = value;
                }
            }

            public Board Board
            {
                get
                {
                    return m_Board;
                }

                set
                {
                    m_Board = value;
                }
            }

            public Game.WhoIsPlaying CurrPlayer
            {
                get
                {
                    return m_CurrPlayer;
                }

                set
                {
                    m_CurrPlayer = value;
                }
            }
        }

        public static State MakeNewState(State i_PrevState, Cell i_CellToAdd)
        {
            State newState = new State(i_PrevState);
            char playerSymbol = i_PrevState.CurrPlayer == Game.WhoIsPlaying.Computer ? 'X' : 'O';
            newState.m_Board.SetCell(i_CellToAdd, playerSymbol);
            newState.m_LastAddedCell = i_CellToAdd;
            newState.CurrPlayer = newState.CurrPlayer == Game.WhoIsPlaying.Computer ? Game.WhoIsPlaying.Player1 : Game.WhoIsPlaying.Computer;
            return newState;
        }

        public static int StateVal(State i_State)
        {
            int val = 0;
            if (i_State.m_Board.CheckPlayerLoss(i_State.m_LastAddedCell, 'O'))
            {
                val = 1;
            }
            else if (i_State.m_Board.CheckPlayerLoss(i_State.m_LastAddedCell, 'X'))
            {
                val = -1;
            }

            return val;
        }

        public static Cell MiniMaxDecision(State i_State)
        {
            int val = -2, row = 1, col = 1;
            Cell firstTieCell = new Cell(), firstLossCell = new Cell();
            bool foundWin = false, foundTie = false, foundLoss = false;
            State newState = new State();
            Cell Cell = new Cell();

            while ((val != 1) && (row <= i_State.m_Board.Size))
            {
                while (col <= i_State.m_Board.Size)
                {
                    Cell = new Cell(row, col);

                    if (i_State.m_Board.IsEmptyCell(Cell))
                    {
                        newState = MakeNewState(i_State, Cell);
                        val = MinValue(newState);
                    }

                    if (val == 1)
                    {
                        foundWin = true;

                        break;
                    }

                    if ((val == 0) && (!foundTie))
                    {
                        firstTieCell = Cell;
                        foundTie = true;
                    }
                    else if ((val == -1) && (!foundLoss))
                    {
                        firstLossCell = Cell;
                        foundLoss = true;
                    }

                    col++;
                }

                row++;
                col = 1;
            }

            if (!foundWin)
            {
                if (foundTie)
                {
                    Cell = firstTieCell;
                }
                else
                {
                    Cell = firstLossCell;
                }
            }

            return Cell;
        }

        public static int MaxValue(State i_State)
        {
            if (IsLeaf(i_State.m_Board) || (StateVal(i_State) == 1) || (StateVal(i_State) == -1))
            {
                return StateVal(i_State);
            }

            int val = -2, row = 1, col = 1;
            State newState = new State();

            while ((val != 1) && (row <= i_State.m_Board.Size))
            {
                while ((val != 1) && (col <= i_State.m_Board.Size))
                {
                    Cell newCell = new Cell(row, col);
                    if (i_State.m_Board.IsEmptyCell(newCell))
                    {
                        newState = MakeNewState(i_State, newCell);
                        val = Math.Max(val, MinValue(newState));
                    }

                    col++;
                }

                row++;
                col = 1;
            }

            return val;
        }

        public static int MinValue(State i_State)
        {
            if (IsLeaf(i_State.m_Board) || StateVal(i_State) == 1 || StateVal(i_State) == -1)
            {
                return StateVal(i_State);
            }

            int val = 2, row = 1, col = 1;
            State newState = new State();

            while ((val != 1) && (row <= i_State.m_Board.Size))
            {
                while ((val != 1) && (col <= i_State.m_Board.Size))
                {
                    Cell newCell = new Cell(row, col);
                    if (i_State.m_Board.IsEmptyCell(newCell))
                    {
                        newState = MakeNewState(i_State, newCell);
                        val = Math.Min(val, MaxValue(newState));
                    }

                    col++;
                }

                row++;
                col = 1;
            }

            return val;
        }

        public static bool IsLeaf(Board i_Board)
        { 
            bool isLeaf = true;

            for (int row = 1; row <= i_Board.Size; row++)
            {
                for (int col = 1; col <= i_Board.Size; col++)
                {
                    Cell cell = new Cell(row, col);
                    if (i_Board.IsEmptyCell(cell))
                    {
                        isLeaf = false;
                    }
                }
            }

            return isLeaf;
        }
    }
}
