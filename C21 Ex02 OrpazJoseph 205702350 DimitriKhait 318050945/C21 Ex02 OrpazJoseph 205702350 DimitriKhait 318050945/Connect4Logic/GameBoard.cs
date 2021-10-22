namespace Connect4Logic
{
    public class GameBoard
    {
        private char[,] m_Board;

        public GameBoard(int i_Rows, int i_Columns)
        {
            m_Board = new char[i_Rows, i_Columns];
        }

        // $G$ DSN-999 (-10) You should not expose a reference to a private member (array is a reference type); breaking class encapsulation.
        public char[,] Board
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
    }
}
