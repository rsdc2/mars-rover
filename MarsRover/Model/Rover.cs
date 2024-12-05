using MarsRover.Data;
using MarsRover.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Model
{
    internal class Rover
    {
        public static int Rovers { get; private set; } = 0;
        public RoverPosition Position { get; private set; }

        public int Id { get; private set; }
        public int X { get => Position.X; }
        public int Y { get => Position.Y; }

        public Direction Direction 
        { 
            get => Position.Direction;
            set => Position.Direction = value;
        }

        public Rover(RoverPosition position) 
        {
            Position = position;
            Rovers++;
            Id = Rovers;
        }  

        public Either<Direction> Rotate(RotateInstruction rotation)
        {
            var directionInt = (int)Direction;
            var rotationInt = (int)rotation;

            var newDirection = (Direction)((4 + directionInt + rotationInt) % 4);
            Direction = newDirection;
            return Either<Direction>.From(Direction);
        }

    }
}
