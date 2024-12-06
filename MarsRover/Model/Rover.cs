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
        public static int RoverCount { get; private set; } = 0;
        public RoverPosition Position { get; private set; }

        public int Id { get; private set; }
        public int X { get => Position.X; }
        public int Y { get => Position.Y; }

        public Direction Direction 
        { 
            get => Position.Direction;
        }

        public Rover(RoverPosition position) 
        {
            Position = position;
            RoverCount++;
            Id = RoverCount;
        }

        public static Rover From(int x, int y, Direction direction) =>
            new Rover(RoverPosition.From(x, y, direction));

        public static Rover From(RoverPosition position) => new Rover(position);
 
        /// <summary>
        /// Get new position of the rover when it moves one block in the direction that it is facing
        /// </summary>
        /// <returns></returns>
        public Either<RoverPosition> GetNewPosition() => Position.Triple switch
        {
            (_, 0, Direction.S) => Either<RoverPosition>.From(Messages.CannotMoveRover(Id)),
            (0, _, Direction.W) => Either<RoverPosition>.From(Messages.CannotMoveRover(Id)),
            (_, _, Direction.E) => Either<RoverPosition>.From(Position with { X = Position.X + 1 }),
            (_, _, Direction.W) => Either<RoverPosition>.From(Position with { X = Position.X - 1 }),
            (_, _, Direction.N) => Either<RoverPosition>.From(Position with { Y = Position.Y + 1 }),
            (_, _, Direction.S) => Either<RoverPosition>.From(Position with { Y = Position.Y - 1 }),
            _ => Either<RoverPosition>.From(Messages.CannotMoveRover(Id))
        };

        /// <summary>
        /// Move the Rover one unit in the direction that it is facing
        /// </summary>
        /// <returns></returns>
        public Either<Rover> Move()
        {
            var newPosition = GetNewPosition();
            if (newPosition.Value is Success<RoverPosition> success)
            {
                Position = success.Value;
                return Either<Rover>.From(this);
            }

            return Either<Rover>.From(newPosition.Message);
        }

        public Either<Rover> Rotate(RotateInstruction rotation)
        {
            var directionInt = (int)Direction;
            var rotationInt = (int)rotation;

            var newDirection = (Direction)((4 + directionInt + rotationInt) % 4);
            Position = Position with { Direction = newDirection };
            return Either<Rover>.From(this);
        }


        public Either<Rover> UpdateX(int x)
        {
            if (x < 0)
            {
                return Either<Rover>.From(Messages.CannotMoveRover(Id));
            }
            Position = Position with { X = x };
            return Either<Rover>.From(this);
        }

        public Either<Rover> UpdateY(int y)
        {
            if (y < 0)
            {
                return Either<Rover>.From(Messages.CannotMoveRover(Id));
            }
            Position = Position with { Y = y };
            return Either<Rover>.From(this);
        }


    }
}
