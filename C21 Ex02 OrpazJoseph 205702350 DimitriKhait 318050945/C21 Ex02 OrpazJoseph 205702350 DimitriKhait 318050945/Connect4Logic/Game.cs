using System;

namespace Connect4Logic
{
    public class Game
    {
        // Decides who will start first
        public static int WhoPlayFirst()
        {
            Random rnd = new Random();

            return rnd.Next(1, 3);
        }

        // If game is against PC, makes random moves and set the value if it's a valid move
        public static void SetPcMove(GameBoard i_Matrix, PlayerInfo i_CurrentPlayer, PlayerInfo i_Opponent)
        {
            int cols = i_Matrix.Board.GetLength(1);

            if (i_CurrentPlayer.IsPc)
            {
                while (true)
                {
                    Random pcMove = new Random();
                    int move = pcMove.Next(1, cols + 1);

                    if (InputValidation.checkPlace(i_Matrix, move, i_CurrentPlayer))
                    {
                        return;
                    }
                }
            }
        }

        // Get user input and send to check move function
        // $G$ DSN-001 (-3) Instead of returning arbitrary int literals; use enum or further separate to methods with meaningful names
        // $G$ CSS-028 (-3) A method should not include more than one return statement.
        public static int SetPlayerMove(GameBoard i_Matrix, PlayerInfo i_CurrentPlayer, PlayerInfo i_Opponent, string i_Input)
        {
            while (true)
            {
                int num;

                if (int.TryParse(i_Input, out num))
                {
                    if (num <= 0 || num > i_Matrix.Board.GetLength(1))
                    {
                        // Number not between [1 - columns] size
                        return 2;
                    }

                    if (InputValidation.checkPlace(i_Matrix, num, i_CurrentPlayer))
                    {
                        return 1;
                    }

                    // No more place to put, try another place.
                    return 3;
                }

                // Input is invalid
                return 4;
            }
        }

        // $G$ CSS-028 (-5) A method should not include more than one return statement.
        public static bool CheckIfWin(GameBoard i_Matrix, PlayerInfo i_CurrentPlayer)
        {
            char input = i_CurrentPlayer.PlayerId;
            int rows = i_Matrix.Board.GetLength(0) - 1;
            int cols = i_Matrix.Board.GetLength(1) - 1;

            // Horizontal check
            for (int i = rows; i >= 0; i--)
            {
                for (int j = cols; j > 2; j--)
                {
                    if (i_Matrix.Board[i, j] == input && i_Matrix.Board[i, j - 1] == input && i_Matrix.Board[i, j - 2] == input && i_Matrix.Board[i, j - 3] == input)
                    {
                        return true;
                    }
                }
            }

            // Vertical check
            for (int i = rows; i > 2; i--)
            {
                for (int j = cols; j >= 0; j--)
                {
                    if (i_Matrix.Board[i, j] == input && i_Matrix.Board[i - 1, j] == input && i_Matrix.Board[i - 2, j] == input && i_Matrix.Board[i - 3, j] == input)
                    {
                        return true;
                    }
                }
            }

            // Diagonally check
            for (int i = rows; i > 2; i--)
            {
                for (int j = cols; j > 2; j--)
                {
                    if (i_Matrix.Board[i, j] == input && i_Matrix.Board[i - 1, j - 1] == input && i_Matrix.Board[i - 2, j - 2] == input && i_Matrix.Board[i - 3, j - 3] == input)
                    {
                        return true;
                    }

                    if (i_Matrix.Board[i, j - 3] == input && i_Matrix.Board[i - 1, j - 2] == input && i_Matrix.Board[i - 2, j - 1] == input && i_Matrix.Board[i - 3, j] == input)
                    {
                        // Reverse Diagonally check
                        return true;
                    }
                }
            }

            return false;
        }

        // Checks if game board is full
        public static bool CheckBoardFull(GameBoard i_Matrix)
        {
            int countChars = 0;

            for (int i = 0; i < i_Matrix.Board.GetLength(1); i++)
            {
                if (i_Matrix.Board[0, i] != ' ')
                {
                    countChars++;
                }
            }

            return countChars == i_Matrix.Board.GetLength(1);
        }
    }
}
