using System;
using System.Linq;
using System.Threading;

namespace Game_Of_Life
{
    internal static class Program
    {
        public static void Main()
        {
            Console.WriteLine("What type of the game you prefer?\n" +
                              "Write \"1\" to set lonely glider\n" +
                              "Write not \"1\" to set chaos on the screen\n");
            
            int.TryParse(Console.ReadLine(), out var gameType);
            Console.Clear();
            
            Console.WriteLine("Input size of map\n");
            if (!int.TryParse(Console.ReadLine(), out var gameSize))
            {
                Console.Clear();
                Console.WriteLine("This is not a size!\n" +
                                  "will be set default size : 10");
                gameSize = 10;
                Thread.Sleep(1500);
            }

            if (gameSize < 3)
            {
                Console.Clear();
                Console.WriteLine("Size is too small for game of life\n" +
                                  "it will be set default size : 10");
                gameSize = 10;
                Thread.Sleep(1500);
            }
            Console.Clear();
            
            var gameOfLife = new GameOfLife(gameSize);
            gameOfLife.SetGameMapType(gameType);
            gameOfLife.Start();
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }

    internal class GameOfLife
    {
        private Map _cellMap;
        
        private readonly int _size;
        public GameOfLife(int size)
        {
            _size = size;
            _cellMap = new Map(size);
        }

        public void Start()
        {
            _cellMap.Print(0, false);
            var generation = 1;
            for(;; generation++)
            {
                if (!_cellMap.IsMapNotDead())
                    break;
                
                Console.SetCursorPosition(0,0);
                _cellMap.Print(generation, true);
                var newMap = new Map(_size);
                for (var i = 0; i < _size; i++)
                {
                    for (var j = 0; j < _size; j++)
                    {
                        var countNeighbours = _cellMap.GetNeighboursCount(i, j);
                        if (_cellMap.GetCellStatus(i, j))
                        {
                            newMap.SetValue(countNeighbours == 3 || countNeighbours == 2, i, j);
                        }
                        else
                        {
                            newMap.SetValue(countNeighbours == 3, i, j);
                        }
                    }
                }
                _cellMap = newMap;
                
                Thread.Sleep(100);
            }
            
            Console.Clear();
            _cellMap.Print(generation, false);
            Console.WriteLine("Game ended at {0} generation", generation);
        }

        public void SetGameMapType(int gameType) => _cellMap.SetGameState(gameType);

        private class Map
         {
             private readonly int _size;
             private readonly bool[,] _cellMap;

             public Map(int size)
             {
                 _size = size;
                 _cellMap = new bool[size, size];
                 var rand = new Random(size);
                 for (var i = 0; i < size; i++)
                 {
                     for (var j = 0; j < size; j++)
                     {
                         _cellMap[i, j] = rand.Next() % 2 == 0;
                     }
                 }
             }

             public void SetValue(bool value, int i, int j) => _cellMap.SetValue(value, i, j);
             
             public bool GetCellStatus(int y, int x)
             {
                 var tempX = x >= 0 ? x % _size : _size - Math.Abs(x % _size);
                 var tempY = y >= 0 ? y % _size : _size - Math.Abs(y % _size);
                 return _cellMap[tempX, tempY];
             }

             public int GetNeighboursCount(int y, int x)
             {
                 var counter = 0;
                 for (var i = x - 1; i < x + 2; i++)
                 {
                     for (var j = y - 1; j < y + 2; j++)
                     {
                         if( i == x && j == y)
                             continue;
                         counter += GetCellStatus(j, i)  ? 1 : 0;
                     }
                 }
                 return counter;
             }

             public void Print (int generation,  bool printGeneration)
             {
                 var printTemplate =  "";
                 for (var i = 0; i < _size; i++)
                 {
                     for (var j = 0; j < _size; j++)
                     {
                         printTemplate += _cellMap[i, j] ? "*" : "_";
                     }
                     printTemplate += '\n';
                 }

                 printTemplate += printGeneration ? "generation {0}\n" : "";
                 
                 Console.WriteLine(printTemplate, generation);
             }

             public void SetGameState(int gameType)
             {
                 switch (gameType)
                 {
                     case 1:
                     {
                         for (var i = 0; i < _size; i++)
                         {
                             for (var j = 0; j < _size; j++)
                             {
                                 _cellMap[i, j] = false;
                             }
                         }
                         _cellMap[1, 0] = true;
                         _cellMap[1, 2] = true;
                         _cellMap[0, 2] = true;
                         _cellMap[2, 1] = true;
                         _cellMap[2, 2] = true;
                         break;
                     }
                     default: break;
                 }


                 //Glider
                 
             }

             public bool IsMapNotDead()=> _cellMap.Cast<bool>().Any(cell => cell);
             
         }
    }
}