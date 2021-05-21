using System;
using System.Text;

namespace B21_Ex02
{
    public class Game
    {
        private Board m_Board;
        private TypeOfGame m_Type;
        private Player m_Player1;
        private Player m_Player2;
        private GameStatus m_GameStatus;
        private WhoIsPlaying m_CurrPlayer;
        private int m_CountTurns;
        private UI m_Ui;

        public Game(UI i_GameUserInterface)
        {
            m_Ui = i_GameUserInterface;
            m_GameStatus = GameStatus.On;
            m_CurrPlayer = WhoIsPlaying.Player1;
            m_CountTurns = 0;
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

        public Player Player1
        {
            get
            {
                return m_Player1;
            }

            set
            {
                m_Player1 = value;
            }
        }

        public Player Player2
        {
            get
            {
                return m_Player2;
            }

            set
            {
                m_Player2 = value;
            }
        }

        public void Init()
        {
            int boardSize = m_Ui.GetBoardSizeFromUser();
            m_Board = new Board(boardSize);
            m_Player1 = new Player(WhoIsPlaying.Player1, 'O');
            m_Ui.PrintMenu();
            TypeOfGame type = m_Ui.GetTypeOfGame();
            m_Type = type;
            if (m_Type == TypeOfGame.PlayerVsComputer)
            {
                m_Player2 = new Player(WhoIsPlaying.Computer, 'X');
            }
            else
            {
                m_Player2 = new Player(WhoIsPlaying.Player2, 'X');
            }
        }

        public void RunGame()
        {
            bool endOfGame = false;

            while (!endOfGame)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                m_Ui.Printboard(m_Board);
                PlayCurrGame();
                ShowScores();
                CheakIfEndOfGame(ref endOfGame);
            }
        }

        private void ShowScores()
        {
            m_Ui.PrintScoreTable(m_Player1.Scores, m_Player2.Scores);
        }

        private void CheakIfEndOfGame(ref bool io_EndOfGame)
        {
            if (m_Ui.IsAnotherGame())
            {
                ResetGame();
            }
            else
            {
                io_EndOfGame = true;
                m_Ui.PrintToScreen("======= GOOD BYE :) ======");
            }
        }

        private void PlayCurrGame()
        {
            while (m_GameStatus == GameStatus.On)
            {
                bool isEnteredQ = false;
                bool isFirstTry = true;
                bool isCellOk = false;
                Cell playerChoiseInput = new Cell();

                if (m_CurrPlayer == WhoIsPlaying.Computer)
                {
                    ComputerCellChoise(ref playerChoiseInput);
                    MakeComputerTurn(playerChoiseInput);
                }
                else
                {
                    while (!isCellOk)
                    {
                        playerChoiseInput = m_Ui.GetPlayerChoise(m_CurrPlayer, ref isFirstTry, ref isEnteredQ);

                        if (isEnteredQ)
                        {
                            HandleQSituation();
                            break;
                        }

                        MakeHumanTurn(playerChoiseInput, ref isCellOk);
                    }
                }

                Ex02.ConsoleUtils.Screen.Clear();
                m_Ui.Printboard(Board);
            }
        }

        private void ComputerCellChoise(ref Cell io_CellChoise)
        {
            if(m_Board.Size == 9)
            {
                io_CellChoise = GetRandomMove();
            }
            else if ((m_Board.Size == 7 || m_Board.Size == 8) && (m_CountTurns < (m_Board.Size * m_Board.Size) * 7 / 8))
            {
                io_CellChoise = GetRandomMove();
            }
            else if ((m_Board.Size == 6) && (m_CountTurns < (m_Board.Size * m_Board.Size) * 3 / 4))
            {
                io_CellChoise = GetRandomMove();
            }
            else if ((m_Board.Size == 5) && (m_CountTurns < (m_Board.Size * m_Board.Size) / 2))
            {
                io_CellChoise = GetRandomMove();
            }
            else
            {
                Board copyBoard = new Board(m_Board.Size);
                copyBoard.CopyBoard(m_Board);
                AI.State state = new AI.State(copyBoard);
                io_CellChoise = AI.MiniMaxDecision(state);
            }
        }

        private Cell GetRandomMove()
        {
            Cell newCell = new Cell(1, 1);
            Random rnd = new Random();
            int row = 1, col = 1;

            while(!m_Board.IsEmptyCell(newCell))
            {
                row = rnd.Next(1, m_Board.Size + 1);
                col = rnd.Next(1, m_Board.Size + 1);
                newCell.Row = row;
                newCell.Col = col;
            }

            return newCell;
        }

        private int CountTurns
        {
            get
            {
                return m_CountTurns;
            }

            set
            {
                m_CountTurns = value;
            }
        }

        private WhoIsPlaying CurrPlayer
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

        private void updateGameStatus(Cell i_CellChoose)
        {
            ////Enough turns have passed to make win
            if (m_CountTurns >= (m_Board.Size * 2) - 1) 
            {
                char symbol = m_CurrPlayer == WhoIsPlaying.Player1 ? 'O' : 'X';
                if (m_Board.CheckPlayerLoss(i_CellChoose, symbol))
                {
                    SetScoreToWinner(m_CurrPlayer == WhoIsPlaying.Player1 ? WhoIsPlaying.Player2 : WhoIsPlaying.Player1);
                    m_GameStatus = GameStatus.Win;
                }
                else if (m_CountTurns == m_Board.Size * m_Board.Size)
                {
                    m_GameStatus = GameStatus.Tie;
                }
            }
        }

        private void MakeComputerTurn(Cell i_CellChoose)
        {
            SetSymbolInCell(i_CellChoose, m_CurrPlayer);
            m_CountTurns++;
            updateGameStatus(i_CellChoose);
            SwitchPlayers();
        }

        private void MakeHumanTurn(Cell i_CellChoose, ref bool i_CellOk)
        {
            if (!m_Board.IsCellOk(i_CellChoose))
            {
                return;
            }

            i_CellOk = true;
            SetSymbolInCell(i_CellChoose, m_CurrPlayer);
            m_CountTurns++;
            updateGameStatus(i_CellChoose);
            SwitchPlayers();
        }

        private void SwitchPlayers()
        {
            if (m_Type == TypeOfGame.TwoPlayers)
            {
                m_CurrPlayer = m_CurrPlayer == WhoIsPlaying.Player1 ? WhoIsPlaying.Player2 : WhoIsPlaying.Player1;
            }
            else
            {
                m_CurrPlayer = m_CurrPlayer == WhoIsPlaying.Player1 ? WhoIsPlaying.Computer : WhoIsPlaying.Player1;
            }
        }

        private void HandleQSituation()
        {
            SetScoreToWinner(m_CurrPlayer == WhoIsPlaying.Player1 ? WhoIsPlaying.Player2 : WhoIsPlaying.Player1);
           m_GameStatus = GameStatus.Quit;
        }

        private void SetScoreToWinner(WhoIsPlaying i_Player)
        {
            if(i_Player == WhoIsPlaying.Player1)
            {
                Player1.Scores += 1;
            }
            else
            {
                Player2.Scores += 1;
            }
        }

        private void SetSymbolInCell(Cell i_ChosenCell, WhoIsPlaying i_Player)
        {
            m_Board.SetCell(i_ChosenCell, i_Player == WhoIsPlaying.Player1 ? m_Player1.Symbol : m_Player2.Symbol);
        }

        private void ResetGame()
        {
            m_Board.ClearBoard();
            m_CountTurns = 0;
            m_GameStatus = GameStatus.On;
            m_CurrPlayer = WhoIsPlaying.Player1;
        }

        public enum TypeOfGame
        {
            TwoPlayers = 1,
            PlayerVsComputer
        }

        public enum GameStatus
        {
            On = 1,
            Win,
            Tie,
            Quit
        }

        public enum WhoIsPlaying
        {
            Player1 = 1,
            Player2,
            Computer
        }
    }
}