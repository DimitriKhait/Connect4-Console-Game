using System;
using Connect4Logic;
using ClearScreen = Ex02.ConsoleUtils;

namespace Connect4UI
{
    // $G$ CSS-999 (-3) Class with all static methods should be a static class
    public class GameUI
    {
        // $G$ CSS-999 (-2) Only private methods should start with lower-case letter
        // $G$ CSS-999 (-3) The name of the method is not meaningful enough compared to its function.
        // $G$ CSS-999 (-1) Duplicate lines where if conditions could be further minimized and more readable.
        internal static void welcomeScreen()
        {
            Console.WriteLine("Hello and welcome to Connect 4 game.");
            while (true)
            {
                Console.WriteLine("Type 1 to see information. Type 2 to start a game.");
                // $G$ CSS-000 (-3) Variable name is not meaningful and understandable
                string welcome = Console.ReadLine();
                int num;

                if (int.TryParse(welcome, out num))
                {
                    if (num == 1)
                    {
                        showInformation();
                        while (true)
                        {
                            Console.WriteLine("Would you like to start a game? press Y for yes, N to quit.");
                            string temp = Console.ReadLine();

                            if (temp == "y" || temp == "Y")
                            {
                                initializeGame();
                            }

                            if (temp == "N" || temp == "n")
                            {
                                Environment.Exit(0);
                            }

                            Console.WriteLine("Please type Y or N only.");
                        }
                    }

                    if (num == 2)
                    {
                        initializeGame();
                    }

                    Console.WriteLine("Please type 1 or 2 only.");
                }

                Console.WriteLine("Please type 1 or 2 only.");
            }
        }

        private static void showInformation()
        {
            Console.WriteLine(@"
==============================================================================

In this game you need to connect 4 pieces (Horizontal / Vertical / Diagonally).
Player no.1 will be X's (X), and Player no.2 will be O's (O).
If you decide to play against the Computer, then you will be X's (X).
In every moment of the game you can quit the current game by typing Q.

==============================================================================
");
        }

        // Get all primary info from player1 such as name, board size, game mode (PvP or PvPC)
        private static void initializeGame()
        {
            ClearScreen.Screen.Clear();
            Console.WriteLine("Please enter your name: ");
            string name = Console.ReadLine();

            // $G$ NTT-999 (-2) Here you should have used verbatim string ('@')
            Console.WriteLine($"Hello {name}");
            Console.WriteLine("You need to choose the board size. Must be between (4x4) up to (8x8).");
            Console.WriteLine("Please enter the size of the rows: ");
            int rows;

            while (true)
            {
                // $G$ CSS-000 (-3) Variable name is not meaningful and understandable
                string temp = Console.ReadLine();

                if (InputValidation.CheckBoardSize(temp))
                {
                    rows = int.Parse(temp);
                    break;
                }

                // $G$ CSS-999 (-2) You should use constant vars and not literal values; literals 4 & 8.
                Console.WriteLine("Must be between 4x4 up to 8x8. Please try again.");
            }

            Console.WriteLine("Please enter the size of the cols: ");
            int cols;

            while (true)
            {
                string temp = Console.ReadLine();

                if (InputValidation.CheckBoardSize(temp))
                {
                    cols = int.Parse(temp);
                    break;
                }

                Console.WriteLine("Must be between 4x4 up to 8x8. Please try again.");
            }

            Console.WriteLine($"Number of rows: {rows}, Number of cols: {cols}");
            GameBoard board = new GameBoard(rows, cols);

            // $G$ DSN-001 (-5) Tokens ('X','O') should be represented by either a struct, a class or an enum.
            Console.WriteLine("Against who do you want to play? press 1 for player2, press 2 for computer");
            PlayerInfo playerOne = new PlayerInfo(name, 'X', false);
            PlayerInfo playerTwo = new PlayerInfo('O');

            while (true)
            {
                int temp;

                if (int.TryParse(Console.ReadLine(), out temp))
                {
                    // $G$ CSS-000 (-3) Variable name is not meaningful and understandable
                    if (temp == 1)
                    {
                        Console.WriteLine("Please enter Player2 name: ");
                        string name2 = Console.ReadLine();

                        Console.WriteLine($"Hello: {name2}");
                        playerTwo.PlayerName = name2;
                        playerTwo.IsPc = false;
                        break;
                    }

                    if (temp == 2)
                    {
                        playerTwo.PlayerName = "Computer";
                        playerTwo.IsPc = true;
                        break;
                    }
                }

                Console.WriteLine("Your input is invalid. Please enter 1 for player2, 2 for Computer.");
            }

            BoardGameCreator.createBoard(board, rows, cols);
            gameInProgress(board, playerOne, playerTwo);
        }

        // Makes a separation between player vs player and player vs PC.
        // Also checks who play first and act accordingly. 
        private static void gameInProgress(GameBoard i_Matrix, PlayerInfo i_Name1, PlayerInfo i_Name2)
        {
            int playFirst;

            if (i_Name2.IsPc)
            {
                playFirst = 1;
            }
            else
            {
                playFirst = Game.WhoPlayFirst();
            }

            if (playFirst == 1)
            {
                Console.WriteLine($"{i_Name1.PlayerName} play first.");
                currentGame(i_Matrix, i_Name1, i_Name2);
            }
            else if (playFirst == 2)
            {
                Console.WriteLine($"{i_Name2.PlayerName} play first.");
                currentGame(i_Matrix, i_Name2, i_Name1);
            }
        }

        // In each move the method checks if the move is valid.
        // Also checks if it's a win or a draw and in this case checks if the user wants to play another game. 
        private static void currentGame(GameBoard i_Matrix, PlayerInfo i_PlayFirst, PlayerInfo i_PlaySecond)
        {
            while (true)
            {
                Console.WriteLine($"{i_PlayFirst.PlayerName} Enter your choice: 1 - {i_Matrix.Board.GetLength(1)}, press Q to forfeit anytime.");
                if (Game.CheckBoardFull(i_Matrix))
                {
                    Console.WriteLine("The board is full. Its A Draw!!");
                    playAgain(i_Matrix, i_PlayFirst, i_PlaySecond);
                }

                checkPlayerMove(i_Matrix, i_PlayFirst, i_PlaySecond);
                if (Game.CheckIfWin(i_Matrix, i_PlayFirst))
                {
                    Console.WriteLine($"{i_PlayFirst.PlayerName} connected 4 in a row with {i_PlayFirst.PlayerId}");
                    i_PlayFirst.PlayerScore++;
                    playAgain(i_Matrix, i_PlayFirst, i_PlaySecond);
                }

                if (Game.CheckBoardFull(i_Matrix))
                {
                    Console.WriteLine("The board is full. Its A Draw!!");
                    playAgain(i_Matrix, i_PlayFirst, i_PlaySecond);
                }

                if (!i_PlaySecond.IsPc)
                {
                    Console.WriteLine($"{i_PlaySecond.PlayerName} Enter your choice: 1 - {i_Matrix.Board.GetLength(1)}, press Q to forfeit anytime.");
                    checkPlayerMove(i_Matrix, i_PlaySecond, i_PlayFirst);
                }
                else
                {
                    Game.SetPcMove(i_Matrix, i_PlaySecond, i_PlayFirst);
                    ClearScreen.Screen.Clear();
                    BoardGameCreator.createBoard(i_Matrix, i_Matrix.Board.GetLength(0), i_Matrix.Board.GetLength(1));
                }

                if (Game.CheckIfWin(i_Matrix, i_PlaySecond))
                {
                    Console.WriteLine($"{i_PlaySecond.PlayerName} connected 4 in a row with {i_PlaySecond.PlayerId}.");
                    i_PlaySecond.PlayerScore++;
                    playAgain(i_Matrix, i_PlayFirst, i_PlaySecond);
                }
            }
        }

        // Asks the player if they want to play another game and closes the console window if they choose not to. 
        private static void playAgain(GameBoard i_Matrix, PlayerInfo i_Name1, PlayerInfo i_Name2)
        {
            Console.WriteLine($"{i_Name1.PlayerName} score: {i_Name1.PlayerScore} {i_Name2.PlayerName} score: {i_Name2.PlayerScore}.");
            Console.WriteLine("play again? press Y for yes, N for no.");
            while (true)
            {
                string answer = Console.ReadLine();

                if (answer == "Y" || answer == "y")
                {
                    ClearScreen.Screen.Clear();
                    GameBoard newBoard = new GameBoard(i_Matrix.Board.GetLength(0), i_Matrix.Board.GetLength(1));

                    BoardGameCreator.createBoard(newBoard, i_Matrix.Board.GetLength(0), i_Matrix.Board.GetLength(1));
                    gameInProgress(newBoard, i_Name1, i_Name2);
                }
                else if (answer == "N" || answer == "n")
                {
                    Console.WriteLine("GoodBye.");
                    // $G$ CSS-999 (-3) Really bad practice to use The Exit method to terminate application outside Program's static Main method; flow should return back to main.
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Please type only Y or N.");
                }
            }
        }

        // Checks if player did a valid move and presents specific message for each problem.
        // In addition, checks if player wants to quit the current game.
        private static void checkPlayerMove(GameBoard i_Matrix, PlayerInfo i_Name1, PlayerInfo i_Name2)
        {
            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput == "q" || userInput == "Q")
                {
                    i_Name2.PlayerScore++;
                    playAgain(i_Matrix, i_Name1, i_Name2);
                }

                int checkMove = Game.SetPlayerMove(i_Matrix, i_Name1, i_Name2, userInput);

                if (checkMove == 2)
                {
                    Console.WriteLine($"Input is invalid number must be between 1 and {i_Matrix.Board.GetLength(1)}.");
                }
                else if (checkMove == 3)
                {
                    Console.WriteLine("No more place to put, try another place.");
                }
                else if (checkMove == 4)
                {
                    Console.WriteLine("Input is invalid.");
                }
                else
                {
                    ClearScreen.Screen.Clear();
                    BoardGameCreator.createBoard(i_Matrix, i_Matrix.Board.GetLength(0), i_Matrix.Board.GetLength(1));
                    break;
                }
            }
        }
    }
}