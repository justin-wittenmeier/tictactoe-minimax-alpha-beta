using System;

namespace csharp_tictactoe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            while (true)
            {
                game.RunGame();
            }
            
        }
    }
}
