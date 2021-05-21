using System;

namespace B21_Ex02
{
    public class UI
    {
        private Input m_Input;
        private Output m_Output;

        public UI()
        {
            m_Input = new Input();
            m_Output = new Output();
        }

        internal Game.TypeOfGame GetTypeOfGame()
        {
            return m_Input.GetTypeOfGame();
        }

        internal int GetBoardSizeFromUser()
        {
            return m_Input.GetBoardSizeFromUser();
        }

        internal void Printboard(Board i_Board)
        {
            m_Output.Printboard(i_Board);
        }

        internal Cell GetPlayerChoise(Game.WhoIsPlaying i_Player, ref bool io_FirstTry, ref bool io_IsEnterQ)
        {
            return m_Input.GetPlayerChoise(i_Player, ref io_FirstTry, ref io_IsEnterQ);
        }

        internal void PrintScoreTable(int i_Player1Scores, int i_Player2Scores)
        {
            m_Output.PrintScoreTable(i_Player1Scores, i_Player2Scores);
        }

        internal void PrintMenu()
        {
            m_Output.PrintMenu();
        }

        internal bool IsAnotherGame()
        {
           return m_Input.IsAnotherGame();
        }

        internal void PrintToScreen(string i_Str)
        {
           Output.PrintToScreen(i_Str);
        }
    }
}