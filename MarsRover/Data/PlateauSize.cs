using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Data
{
    internal record PlateauSize(int X, int Y)
    {
        public static PlateauSize From(int x, int y)
        {
            return new PlateauSize(x, y);
        }

        public static PlateauSize From((int, int) size)
        {
            return new PlateauSize(size.Item1, size.Item2);
        }
    }
}
