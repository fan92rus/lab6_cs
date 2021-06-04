using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6_cs
{
    class Program
    {
        static void Main(string[] args)
        {
            const int n = 22;

            Drawer drawer = new Drawer(n);
            var dir = Direction.Right;
            int r = 0;
            while (true)
            {
                if (drawer.GetValue(drawer.Position) == 0 && drawer.See(dir, 2) != 1)
                {
                    drawer.SetValue(drawer.Position, 1);
                    r = 0;
                }

                if (drawer.See(dir, 2) == 1 || drawer.See(dir) == -1)
                {
                    dir = drawer.Rotate(dir);
                    r++;
                    if (r > 2)
                        break;
                    continue;
                }

                drawer.Move(dir);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var num = drawer.Arr[j, i];
                    if (num == 1)
                        Console.Write(1);
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }

    class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
    }

    public enum Direction
    {
        Right, Bottom, Left, Up
    }


    class Drawer
    {
        public Drawer(int size)
        {
            Arr = new int[size, size];
        }
        public int[,] Arr;

        public int GetValue(Point point)
        {
            if (point == null)
                return -1;
            return Arr[point.X, point.Y];
        }
        public void SetValue(Point point, int val)
        {
            Arr[point.X, point.Y] = val;
        }

        public Point Position { get; set; } = new Point(0, 0);

        public bool Move(Direction direction)
        {
            var next = GetNextPosition(direction);
            if (next != null)
            {
                this.Position = next;
                return true;
            }

            return false;
        }

        public Point GetNextPosition(Direction direction, int i = 1)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (Position.X - i >= 0)
                    {
                        return new Point(Position.X - i, Position.Y);
                    }
                    break;
                case Direction.Right:
                    if (Position.X < Arr.GetLength(0) - i)
                    {
                        return new Point(Position.X + i, Position.Y);
                    }
                    break;
                case Direction.Up:
                    if (Position.Y - i >= 0)
                    {
                        return new Point(Position.X, Position.Y - i);
                    }
                    break;
                case Direction.Bottom:
                    if (Position.Y < Arr.GetLength(1) - i)
                    {
                        return new Point(Position.X, Position.Y + i);
                    }
                    break;
            }

            return null;
        }

        public int See(Direction direction, int i = 1) => GetValue(GetNextPosition(direction, i));

        public Direction Rotate(Direction direction)
        {
            var next = (((int)direction) + 1) % 4;
            return (Direction)next;
        }
    }
}
