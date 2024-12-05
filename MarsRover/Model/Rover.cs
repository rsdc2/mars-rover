using MarsRover.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Model
{
    internal class Rover
    {
        public RoverPosition Position { get; private set; }

        public int X { get => Position.X; }
        public int Y { get => Position.Y; }

        public Direction D 
        { 
            get => Position.Direction;
            set => Position.Direction = value;
        }

        public Rover(RoverPosition position) 
        {
            Position = position;
        }  

        public Direction Rotate(RotateInstruction rotation)
        {
            var directionInt = (int)D;
            var rotationInt = (int)rotation;

            var newDirection = (Direction)((4 + directionInt + rotationInt) % 4);
            D = newDirection;
            return D;
        }

    }
}
