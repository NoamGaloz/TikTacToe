using System;

namespace B21_Ex02
{
    public class Input
    {
        public Input()
        {
        }

        internal Game.TypeOfGame GetTypeOfGame()
        {
            Game.TypeOfGame res;
            string typeStr = Console.ReadLine();
            int type;
            bool IsIntStr = int.TryParse(typeStr, out type);

            while ((!IsIntStr) || ((type != 1) && (type != 2)))
            {
                Output.PrintToScreen("Invalid Type. Please Enter a new Type Of Game: ");

                typeStr = Console.ReadLine();
                IsIntStr = int.TryParse(typeStr, out type);
                Output.PrintToScreen("\n");
            }

            if (type == 1)
            {
                res = Game.TypeOfGame.TwoPlayers;
            }
            else
            {
                res = Game.TypeOfGame.PlayerVsComputer;
            }

            return res;
        }

        internal int GetBoardSizeFromUser()
        {
            Output.PrintToScreen("Please Enter a Board Size: ");
            string BoardSizeStr = Console.ReadLine();
            int BoardSize;
            bool IsIntStr = int.TryParse(BoardSizeStr, out BoardSize);

            while ((!IsIntStr) || (BoardSize < 3) || (BoardSize > 9))
            {
                Output.PrintToScreen("Invalid Size. Please Enter a new Board Size: ");

                BoardSizeStr = Console.ReadLine();
                IsIntStr = int.TryParse(BoardSizeStr, out BoardSize);
                Output.PrintToScreen("\n");
            }

            return BoardSize;
        }

        internal Cell GetPlayerChoise(Game.WhoIsPlaying i_Player, ref bool io_FirstTry, ref bool io_IsEnterQ)
        {
            bool validCellChoose = false;
            Cell inputCell = new Cell();

            while (!validCellChoose)
            {
                if (io_FirstTry)
                {
                    string whosTurn = string.Format("Player {0} Turn: ", i_Player == Game.WhoIsPlaying.Player1 ? "1" : "2");
                    Output.PrintToScreen(whosTurn);
                }
                else
                {
                    Output.PrintToScreen("ERROR! Your Input Is Not Valid.");
                }

                Output.PrintToScreen("Please Enter Your Next Cell Move,Enter Row and Then Column: ");
                string[] numbersStr = Console.ReadLine().Split();
                io_IsEnterQ = IsEnterQ(numbersStr);

                if(io_IsEnterQ)
                {
                    break;
                }

                int row = 0, col = 0;
                if (IsValidstring(numbersStr, ref row, ref col))
                {
                    validCellChoose = true;
                    inputCell.Row = row;
                    inputCell.Col = col;
                }

                io_FirstTry = false;
            }

            return inputCell;
        }

        private bool IsValidstring(string[] i_Str, ref int i_NumRow, ref int i_NumCol)
        {
            bool isValidDig = false;
        if (i_Str.Length == 2)
        {
            bool isDigRow = int.TryParse(i_Str[0], out i_NumRow);
            bool isDigCol = int.TryParse(i_Str[1], out i_NumCol);
                if (isDigCol && isDigRow)
            {
                isValidDig = true;
            }
        }

        return isValidDig;
        }

        internal bool IsAnotherGame()
        {
            bool anotherGame = true;
            int choise = 0;

            Console.WriteLine(@"Do You Want Another Game?
1. YES!
2. I will quit for now...

Please enter your choise:");

            string userChoise = Console.ReadLine();

            while (!int.TryParse(userChoise, out choise) || ((choise != 1) && (choise != 2)))
            {
                Output.PrintToScreen("ERROR! Please Enter Again: ");
                userChoise = Console.ReadLine();
            }

            anotherGame = choise == 1 ? true : false;

            return anotherGame;
        }

        private bool IsEnterQ(string[] i_Str)
        {
            bool isEnterQ = false;

            if ((i_Str[0].Length == 1) && (i_Str[0] == "q" || i_Str[0] == "Q"))
            {
                isEnterQ = true;
            }

            return isEnterQ;
        }
    }
}