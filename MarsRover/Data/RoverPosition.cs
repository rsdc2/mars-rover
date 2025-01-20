using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Data
{
    internal record RoverPosition(int X, int Y, Direction Direction)
    {
        //public int X = x; 
        //public int Y = y;
        //public Direction Direction = direction;

        public (int, int, Direction) Triple { get => (X, Y, Direction); }

        public static RoverPosition From(int x, int y, Direction direction)
        {
            return new RoverPosition(x, y, direction);
        }

        public static RoverPosition From((int, int, Direction) triple)
        {
            var (x, y, direction) = triple;
            return new RoverPosition(x, y, direction);
        }

        public override string ToString() => $"({X}, {Y}, {Direction})";
    };
}
