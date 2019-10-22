using System;
using System.Threading;

namespace Game_Of_Life
{
    internal static class Program
    {
        public static void Main()
        {
            var gameOfLife = new GameOfLife(50);
            gameOfLife.SetDefaultMap();
            gameOfLife.Start();
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
            _cellMap.Print();
            while (true)
            {
                Console.SetCursorPosition(0,0);
                _cellMap.Print();
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

                /*if (Console.ReadKey(false).Key == ConsoleKey.Escape)
                {
                    break;
                }*/
            }
        }

        public void SetDefaultMap() => _cellMap.SetDefaultState();

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

             public void Print()
             {
                 var printTemplate = "Press Esc to stop Game Of Life";
                 for (var i = 0; i < _size; i++)
                 {
                     for (var j = 0; j < _size; j++)
                     {
                         printTemplate += _cellMap[i, j] ? "*" : "_";
                     }
                     printTemplate += '\n';
                 }
                 
                 Console.WriteLine(printTemplate);
             }

             public void SetDefaultState()
             {
                 for (var i = 0; i < _size; i++)
                 {
                     for (var j = 0; j < _size; j++)
                     {
                         _cellMap[i, j] = false;
                     }
                 }
                 //Minus
                 /*_cellMap[1, 0] = true;
                 _cellMap[1, 1] = true;
                 _cellMap[1, 2] = true;*/
                 
                 //Glider
                 _cellMap[1, 0] = true;
                 _cellMap[1, 2] = true;
                 _cellMap[0, 2] = true;
                 _cellMap[2, 1] = true;
                 _cellMap[2, 2] = true;
             }
         }
    }
}