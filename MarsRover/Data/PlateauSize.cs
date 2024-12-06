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
    }
}
