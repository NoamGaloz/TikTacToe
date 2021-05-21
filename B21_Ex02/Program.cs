using System;

namespace B21_Ex02
{
    public class Program
    {
        public static void Main()
        {
            UI gameUi = new UI();
            Game newGame = new Game(gameUi);
            newGame.Init();
            newGame.RunGame();
        }
    }
}