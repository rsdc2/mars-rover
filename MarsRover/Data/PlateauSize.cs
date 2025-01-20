using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Data
{
    internal record PlateauSize(int X, int Y)
    {
        public static PlateauSize From(int x, int y) => new PlateauSize(x, y);

        public static PlateauSize From((int, int) size) => new PlateauSize(size.Item1, size.Item2);

        public override string ToString() => $"({X}, {Y})";
    }
}
