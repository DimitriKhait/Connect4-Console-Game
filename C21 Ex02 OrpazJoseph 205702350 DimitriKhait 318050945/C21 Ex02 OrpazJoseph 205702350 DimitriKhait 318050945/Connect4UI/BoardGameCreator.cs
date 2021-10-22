using System;

namespace Connect4UI
{
    using Connect4Logic;

    public class BoardGameCreator
    {
        // Creates a Connect 4 board
        // $G$ CSS-999 (-2) Only a private method should start with a lower-case letter
        internal static void createBoard(GameBoard i_Matrix, int i_Rows, int i_Cols)
        {
            for (int k = 0; k < i_Cols; k++)
            {
                Console.Write("  " + (k + 1) + " ");
            }

            Console.WriteLine();
            for (int i = 0; i < i_Rows; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < i_Cols; j++)
                {
                    if (i_Matrix.Board[i, j] != 'X' && i_Matrix.Board[i, j] != 'O')
                    {
                        i_Matrix.Board[i, j] = ' ';
                        Console.Write(i_Matrix.Board[i, j]);
                        Console.Write(" | ");
                    }
                    else
                    {
                        Console.Write(i_Matrix.Board[i, j]);
                        Console.Write(" | ");
                    }
                }

                // $G$ NTT-999 (-2) Here you should have used verbatim string ('@') of string.Format
                Console.WriteLine();
                Console.WriteLine(i_Rows <= 4 ? new string('=', (i_Rows * i_Cols) + 1) : new string('=', (4 * i_Cols) + 1));
            }
        }
    }
}
