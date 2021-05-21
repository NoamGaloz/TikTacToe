using System;

namespace B21_Ex02
{
    public class Player
    {
        private Game.WhoIsPlaying m_Name;
        private char m_Symbol;
        private int m_ScoreNum;

        public Player(Game.WhoIsPlaying name, char symbol)
        {
            m_Name = name;
            m_Symbol = symbol;
            m_ScoreNum = 0;
        }

        public Game.WhoIsPlaying Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }
        
        public char Symbol
        {
            get
            {
                return m_Symbol;
            }

            set
            {
                m_Symbol = value;
            }
        }

        public int Scores
        {
            get
            {
                return m_ScoreNum;
            }

            set
            {
                m_ScoreNum = value;
            }
        }
    }
}
