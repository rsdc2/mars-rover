using MarsRover.Data;
using LanguageExt;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Model
{
    internal class MissionControl
    {
        public List<Rover> Rovers { get; private set; } = [];
        public Plateau Plateau { get; private set; } = Plateau.FromInts(0, 0);
        public MissionControl() { }
        public Either<string, MissionControl> AddPlateau(Plateau plateau)
        {
            Plateau = plateau;
            return Right(this);
        }

        /// <summary>
        /// Adds a rover to the inventory of rovers
        /// </summary>
        /// <param name="rover">A new Rover to add</param>
        /// <returns>An Either of the updated MissionControl</returns>
        public Either<string, MissionControl> AddRover(Rover rover)
        {
            Rovers.Add(rover);
            return Right(this);
        }

        public Either<string, MissionControl> AddRover(RoverPosition position)
        {
            var rover = new Rover(position);
            Rovers.Add(rover);
            return Right(this);
        }

        public static Either<string, MissionControl> FromPlateau(Plateau plateau)
        {
            var missionControl = new MissionControl();
            missionControl.AddPlateau(plateau);
            return Right(missionControl);
        }

        public Either<string, Rover> GetRoverById(int roverId) =>
            Rovers.Where(rover => rover.Id == roverId).ToList().Count switch
            {
                0 => Left(Messages.RoverDoesNotExist(roverId)),
                1 => Right(Rovers.Where(rover => rover.Id == roverId).First()),
                _ => Left(Messages.MoreThanOneRoverWithId(roverId))
            };

        public Either<string, Rover> MoveRover(int roverId)
        {
            return GetRoverById(roverId)
                .Bind(MoveRover);   
        }
        public Either<string, Rover> MoveRover(Rover rover) {
            switch (rover.Position)
            {
                case RoverPosition(_, 0, Direction.S):
                    return Left(Messages.CannotMoveRover(rover.Id));

                case RoverPosition(_, _, Direction.S):
                    return rover.UpdateY(rover.Position.Y - 1);

                case RoverPosition(0, _, Direction.W):
                    return Left(Messages.CannotMoveRover(rover.Id));

                case RoverPosition(_, _, Direction.W):
                    return rover.UpdateX(rover.Position.X - 1);

                case RoverPosition(_, _, Direction.N):
                    if (rover.Position.Y >= Plateau.MaxY)
                    {
                        return Left(Messages.CannotMoveRover(rover.Id));
                    }
                    return rover.UpdateY(rover.Position.Y + 1);

                case RoverPosition(_, _, Direction.E):
                    if (rover.Position.X >= Plateau.MaxX)
                    {
                        return Left(Messages.CannotMoveRover(rover.Id));
                    }
                    return rover.UpdateX(rover.Position.X + 1);

            }
            return Left(Messages.Unforeseen);
        }
        public Either<string, Rover> RotateRover(int roverId, RotateInstruction rotation)
        {
            return GetRoverById(roverId).Bind(rover => rover.Rotate(rotation));
        }
        public string Description()
        {
            var roversStrings = Rovers.Select(rover => rover.Description());
            var roversString = String.Join('\n', roversStrings);
            return $"Mission control status:\n" +
                $"\t- Plateau size: ({Plateau.MaxX}, {Plateau.MaxY})\n" +
                $"\t- Rovers:\n" + roversString;
        }
    }
}
