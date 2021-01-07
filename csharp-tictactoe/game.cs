using System;

namespace csharp_tictactoe
{
    class Game
    {
        public Game()
        {
            Board = new string[9];
            GameOn = false;
            CurrentTurn = "O";
            Wins = new int[,] {{0,1}, {3,1}, {6,1}, {0,3}, {1,3}, {2,3}, {0,4}, {2,2}};
        }

        private string[] Board { get; set; }     
        private bool GameOn { get; set; }
        private string CurrentTurn { get; set; }
        private int[,] Wins { get; set; } 

        public void RunGame()
        {
            GameOn = StartScreen();
            ClearBoard();
            while (GameOn)
            {
                TurnCheck();
                GameOn = WinCheck();
            }
        }

        private void ClearBoard()
        {
            for (int i = 0; i < 9; i++)
            { Board[i] = " "; }
        }
        
        private void PrintBoard()
        { Console.WriteLine($"Current Board:\n{Board[6]}|{Board[7]}|{Board[8]}\n-+-+-\n{Board[3]}|{Board[4]}|{Board[5]}\n-+-+-\n{Board[0]}|{Board[1]}|{Board[2]}"); }

        private void PrintInfo()
        { Console.WriteLine($"Info: \n7|8|9\n-+-+-\n4|5|6\n-+-+-\n1|2|3"); }

        private bool StartScreen()
        {
            Console.WriteLine("\nPress Anything To Start...\n");
            Console.ReadKey();
            return true;
        }

        private void TurnCheck()
        {
            if (CurrentTurn == "O")
            { PlayerMove(); }
            else
            { BotMove(); }
        }

        private void PlayerMove()
        {
            ConsoleKeyInfo input;
            int move;
            bool onOff = true;
            while (onOff)
            {
                PrintBoard();
                Console.WriteLine();
                PrintInfo();
                Console.WriteLine($"Current Turn: {CurrentTurn}");
                Console.Write("Select Move: ");
                input = Console.ReadKey();
                Console.Write("\n\n");
                try
                { move = Int32.Parse(input.KeyChar.ToString()) - 1; }
                catch ( Exception )
                {
                    Console.WriteLine("\nError please only enter integers...\n\n");
                    continue;
                }

                if (move == -1)
                { Console.WriteLine("\nError please select a number between 1 and 9...\n\n"); }
                else if (Board[move] != " ")
                { Console.WriteLine("\nError please only select empty spaces...\n\n"); }
                else
                {
                    onOff = false;
                    Board[move] = "O";
                    CurrentTurn = "X";
                }
            }
        }

        private void BotMove()
        {
            string GameOverBot()
            {
                if (Array.IndexOf(Board, " ") == -1)
                { return "--"; }

                for (int i = 0; i < Wins.GetLength(0); i++)
                {
                    if (Board[Wins[i, 0]] == Board[Wins[i, 0] + Wins[i, 1]] && Board[Wins[i, 0]] == Board[Wins[i, 0] + Wins[i, 1] * 2] && Board[Wins[i, 0]] != " ")
                    { return Board[Wins[i,0]]; }
                }
                return null;
            }

            int[] max(int a, int b)
            {
                int maxi = -2;
                int selected = -1;
                string r = GameOverBot();
                int[] m;
                if (r == "O")
                { return new int[] {-1, 0}; }
                else if (r == "X")
                { return new int[] {2, 0}; }
                else if (r == "--")
                { return new int[] {0, 0}; }

                for (int i = 0; i < Board.Length; i++)
                {
                    if (Board[i] == " ")
                    {
                        Board[i] = "X";
                        m = min(a, b);
                        if (m[0] > maxi)
                        {
                            maxi = m[0];
                            selected = i;
                        }

                        Board[i] = " ";

                        if (maxi >= b)
                        { return new int[] {maxi, selected}; }
                        if (maxi > a)
                        { a = maxi; }
                    }
                }
                return new int[] {maxi, selected};
            }

            int[] min(int a, int b)
            {
                int mini = 2;
                int selected = -1;
                string r = GameOverBot();
                int[] m;
                if (r == "O")
                { return new int[] {-1, 0}; }
                else if (r == "X")
                { return new int[] {1, 0}; }
                else if (r == "--")
                { return new int[] {0, 0}; }
                for (int i = 0; i < Board.Length; i++)
                {
                    if (Board[i] == " ")
                    {
                        Board[i] = "O";
                        m = max(a, b);
                        
                        if (m[0] < mini)
                        {
                            mini = m[0];
                            selected = i;
                        }

                        Board[i] = " ";

                        if (mini <= a)
                        { return new int[] {mini, selected}; }
                        if (mini < b)
                        { b = mini; }
                    }
                }
                return new int[] {mini, selected};
            }
            int[] r = max(-2, 2);
            Board[r[1]] = "X";
            CurrentTurn = "O";
        }

        private bool WinCheck()
        {   
            if (Array.IndexOf(Board, " ") == -1)
            {
                PrintBoard();
                Console.WriteLine("\n*******\n*Draw!*\n*******");
                return false;
            }
            for (int i = 0; i < Wins.GetLength(0); i++)
            {
                if (Board[Wins[i, 0]] == Board[Wins[i, 0] + Wins[i, 1]] && Board[Wins[i, 0]] == Board[Wins[i, 0] + Wins[i, 1] * 2] && Board[Wins[i, 0]] != " ")
                {
                    string winner = Board[Wins[i,0]];
                    PrintBoard();
                    Console.WriteLine($"\n*********\n*{winner} Wins!*\n*********");
                    CurrentTurn = winner;
                    return false;   
                }
            }
            return true;
        }
    }
}
