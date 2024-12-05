using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Data
{
    internal class RoverPosition(int x, int y, Direction direction)
    {
        public int X = x; 
        public int Y = y;
        public Direction Direction = direction;
    };
}
