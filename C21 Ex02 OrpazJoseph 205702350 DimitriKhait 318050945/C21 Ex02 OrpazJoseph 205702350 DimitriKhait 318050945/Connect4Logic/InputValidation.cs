namespace Connect4Logic
{
    public class InputValidation
    {
        // $G$ CSS-028 (-3) A method should not include more than one return statement.
        internal static bool checkPlace(GameBoard i_Matrix, int i_Num, PlayerInfo i_CurrentPlayer)
        {
            int rows = i_Matrix.Board.GetLength(0);

            while (rows != 0)
            {
                if (i_Matrix.Board[rows - 1, i_Num - 1] != 'X' && i_Matrix.Board[rows - 1, i_Num - 1] != 'O')
                {
                    i_Matrix.Board[rows - 1, i_Num - 1] = i_CurrentPlayer.PlayerId;
                    return true;
                }

                rows--;
            }

            return false;
        }

        // $G$ CSS-028 (-3) A method should not include more than one return statement.
        public static bool CheckBoardSize(string i_Size)
        {
            while (true)
            {
                int num;

                if (int.TryParse(i_Size, out num))
                {
                    return num >= 4 && num <= 8;
                }

                return false;
            }
        }
    }
}
