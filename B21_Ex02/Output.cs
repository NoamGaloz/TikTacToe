using System;

namespace B21_Ex02
{
    public class Output
    {
        private char m_PlayerXSymbol;
        private char m_PlayerOSymbol;

        public Output(char i_X = 'X', char i_O = 'O')
        {
            m_PlayerXSymbol = i_X;
            m_PlayerOSymbol = i_O;
        }

        internal static void PrintToScreen(string i_Str)
        {
            Console.WriteLine(i_Str);
        }

        internal void Printboard(Board board)
        {
            Console.Write("  ");
            for (int colNum = 1; colNum <= board.Size; colNum++)
            {
                Console.Write(" ");
                Console.Write(colNum);
                Console.Write("  ");
            }

            Console.Write(@"
");

            for (int rowNum = 1; rowNum <= board.Size; rowNum++)
            {
                Console.Write(rowNum);
                Console.Write("|");

                for (int colNum = 1; colNum <= board.Size; colNum++)
                {
                    Cell currCell = new Cell(rowNum, colNum);
                    
                    Console.Write(" {0} |", GetSymbolToPrint(board.GetCell(currCell)));
                }

                Console.Write(@"
");
                Console.Write("===");
                for (int lineSize = 0; lineSize < board.Size; lineSize++)
                {
                    Console.Write("====");
                }

                Console.Write(@"
");
            }
        }

        private char GetSymbolToPrint(char i_LogicSymbol)
        {
            char symbol;

            if (i_LogicSymbol == '\0')
            {
                symbol = ' ';
            }
            else if (i_LogicSymbol == m_PlayerOSymbol)
            {
                symbol = m_PlayerOSymbol;
            }
            else
            {
                symbol = m_PlayerXSymbol;
            }

            return symbol;
        }

        internal void PrintMenu()
        {
            Console.WriteLine(@"Hello !
Please choose a type of game:
1. Two Players Game.
2. Player vs Computer Game.
");
        }

        internal void PrintScoreTable(int i_Player1Scores, int i_Player2Scores)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(@"---------------- GAME SCORES ----------------");

            Console.Write(
@"==============================================
==      Player 1      ==      Player 2      ==
==============================================
==     {0} Points       ==      {1} Points      ==
==============================================",
i_Player1Scores,
i_Player2Scores);

            Console.Write(@"
                      ||
                      ||
                     \\//
                      \/");

            string winner = i_Player1Scores >= i_Player2Scores ? "Player 1 Lead" : "Player 2 Lead";
            winner = i_Player1Scores == i_Player2Scores ? "ITS A TIE!" : winner;
            Console.WriteLine(
@"
                  {0}
",
winner);
        }
    }
}
