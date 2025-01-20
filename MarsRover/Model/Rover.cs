using MarsRover.Data;
using LanguageExt;
using static LanguageExt.Prelude;
using System;
using System.Diagnostics.Contracts;
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

        public Rover(RoverPosition position, int id) : this(position) 
        {
            Id = id;
        }

        public Rover Copy() => new Rover(new RoverPosition(X, Y, Direction), this.Id);

        public static Rover From(int x, int y, Direction direction) =>
            new Rover(RoverPosition.From(x, y, direction));

        public static Rover From(RoverPosition position) => new Rover(position);
 
        /// <summary>
        /// Get new position of the rover when it moves one block in the direction that it is facing
        /// </summary>
        /// <returns></returns>
        public Either<string, RoverPosition> GetNewPosition() => Position.Triple switch
        {
            (_, 0, Direction.S) => Left(Messages.CannotMoveRoverOffMap(Id)),
            (0, _, Direction.W) => Left(Messages.CannotMoveRoverOffMap(Id)),
            (_, _, Direction.E) => Right(Position with { X = Position.X + 1 }),
            (_, _, Direction.W) => Right(Position with { X = Position.X - 1 }),
            (_, _, Direction.N) => Right(Position with { Y = Position.Y + 1 }),
            (_, _, Direction.S) => Right(Position with { Y = Position.Y - 1 }),
            _ => Left(Messages.CannotMoveRoverOffMap(Id))
        };

        
        /// <summary>
        /// Move the Rover one unit in the direction that it is facing
        /// </summary>
        /// <returns></returns>
        public Either<string, Rover> Move()
        {
            var newPosition = GetNewPosition();
            newPosition.IfRight(position => Position = new RoverPosition(X, Y, Direction));

            return newPosition.Match(
                Left: error => Left(error),
                Right: _ => from pos in newPosition
                            select new Rover(pos)
            );

        }

        public Either<string, Rover> Rotate(RotateInstruction rotation)
        {
            var directionInt = (int)Direction;
            var rotationInt = (int)rotation;

            var newDirection = (Direction)((4 + directionInt + rotationInt) % 4);
            return Right(new Rover(Position with { Direction = newDirection }, this.Id));
        }
        public Either<string, Rover> UpdateX(int x)
        {
            if (x < 0) return Left(Messages.CannotMoveRoverOffMap(Id));
            return Right(new Rover(Position with { X = x }, this.Id));
        }
        public Either<string, Rover> UpdateY(int y)
        {
            if (y < 0) return Left(Messages.CannotMoveRoverOffMap(Id));

            return Right(new Rover(Position with { Y = y }, this.Id));
        }

        public string Description()
        {
            return $"\t\t- Rover {Id} at ({Position.X}, {Position.Y}) " +
                $"facing {Position.Direction}";

        }

    }
}
