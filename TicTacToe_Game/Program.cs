
namespace TicTacToe_Game
{
    internal abstract class Program
    {

        struct Information
        {
            private readonly String _player;
            private readonly bool _won;

            public Information(String player, bool won)
            {
                _player = player;
                _won = won;
            }

            public String GetPlayer()
            {
                return _player;
            }

            public bool Won()
            {
                return _won;
            }
        }
        static void WriteCustomisedArray(String[,] array)
        {
            String line = "";
            int count = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (count == 2)
                    {
                        count = 0;
                        line += array[i, j] + "\n";
                        continue;
                    }
                    
                    line += array[i, j] + " | ";
                    count++;
                }
            }
            
            Console.WriteLine(line);
        }

        static bool WriteToArray(String[,] array, int row, int column, String player)
        {
            row--;
            column--;

            String currentIndex = array[row, column];
            if (currentIndex == "-")
            {
                array[row, column] = player;
                return true;
            }
            else
            {
                WriteCustomisedArray(array);
                Console.WriteLine("Please pick a different spot!");
                Console.WriteLine($"Play as {player}: row column");
                return false;
            }
        }

        static Information CheckIfVerticalWin(String[,] array, int row, int column)
        {
            String currentIndex = array[row, column];
            if (currentIndex == array[0, column] && currentIndex == array[1, column] && currentIndex == array[2, column])
            {
                return new Information(currentIndex, true);
            }

            return new  Information(currentIndex, false);
        }

        static Information CheckIfHorizontalWin(String[,] array, int row, int column)
        {
            String currentIndex = array[row, column];
            if (currentIndex == array[row, 0] && currentIndex == array[row, 1] &&  currentIndex == array[row, 2])
            {
                return new Information(currentIndex, true);
            }
            
            return new Information(currentIndex, false);
        }

        static Information CheckIfDiagonalWin(String[,] array, int row, int column)
        {
            String currentIndex = array[row, column];

            if (currentIndex == array[0, 0] && currentIndex == array[1, 1] && currentIndex == array[2, 2])
            {
                return new Information(currentIndex, true);
            }

            if (currentIndex == array[0, 2] && currentIndex == array[1, 1] && currentIndex == array[2, 0])
            {
                return new Information(currentIndex, true);
            }
            return new Information(currentIndex, false);
        }

        static Information WinChecker(String[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    String player = array[i, j];
                    if (player == "X" || player == "O")
                    {
                        Information verticalWin = CheckIfVerticalWin(array, i, j);
                        Information horizontalWin = CheckIfHorizontalWin(array, i, j);
                        Information diagonalWin = CheckIfDiagonalWin(array, i, j);

                        if (verticalWin.Won())
                        {
                            return new Information(verticalWin.GetPlayer(), verticalWin.Won());
                        }
                        
                        if (horizontalWin.Won())
                        {
                            return new Information(horizontalWin.GetPlayer(), horizontalWin.Won());
                        }
                        
                        if (diagonalWin.Won())
                        {
                            return new Information(diagonalWin.GetPlayer(), diagonalWin.Won());
                        }

                    }
                }
            }
            
            return new Information("No one!", false);
        }
        
        static bool Won(String[,] array)
        {
            Information winChecker = WinChecker(array);
            if (winChecker.Won())
            {
                Console.WriteLine(winChecker.GetPlayer() + " has won!");
                Console.ReadLine();
                Console.Clear();
                return true;
            }

            return false;
        }
        
        static void Main()
        {
            String[,] tttBoardStructure =
            {
                {"-","-","-"},
                {"-","-","-"},
                {"-","-","-"}
            };

            String[,] tttBoard = tttBoardStructure.Clone() as String[,];
            

            while (true)
            {
                WriteCustomisedArray(tttBoard);
                Console.WriteLine("Play as X: row column");
                while (true)
                {
                    var inputFull = Console.ReadLine();
                    var input = inputFull?.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var wroteCorrectly = WriteToArray(tttBoard, Convert.ToInt32(input?[0]), Convert.ToInt32(input?[1]), "X");

                    if (wroteCorrectly)
                    {
                        break;
                    }
                }
                WriteCustomisedArray(tttBoard);

                if (Won(tttBoard))
                {
                    tttBoard = tttBoardStructure.Clone() as String[,];
                    continue;
                }

                Console.WriteLine("Play as O: row column");
                while (true)
                {
                    var inputFull = Console.ReadLine();
                    var input = inputFull?.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var wroteCorrectly = WriteToArray(tttBoard, Convert.ToInt32(input?[0]), Convert.ToInt32(input?[1]), "O");

                    if (wroteCorrectly)
                    {
                        break;
                    }
                }

                WriteCustomisedArray(tttBoard);
                
                if (Won(tttBoard))
                {
                    tttBoard = tttBoardStructure.Clone() as String[,];
                }
            }
        }
    }
}