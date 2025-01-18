using MarsRover.Data;
using LanguageExt;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Input;

namespace MarsRover.Model
{
    internal class MissionControl
    {
        public List<Rover> Rovers { get; private set; } = [];
        public Plateau Plateau { get; private set; } = Plateau.FromInts(0, 0);
        public MissionControl() { }

        public MissionControl(Plateau plateau)
        {
            Plateau = plateau;
        }

        public MissionControl(Plateau plateau, Rover rover)
        {
            Rovers = [rover];
            Plateau = plateau;
        }

        public Either<string, MissionControl> AddPlateau(Plateau plateau) => new MissionControl(plateau);

        /// <summary>
        /// Adds a rover to the inventory of rovers
        /// </summary>
        /// <param name="rover">A new Rover to add</param>
        /// <returns>An Either of the updated MissionControl</returns>
        public Either<string, MissionControl> AddRover(Rover rover) => new MissionControl(Plateau, rover);

        public Either<string, MissionControl> AddRover(RoverPosition position) => AddRover(new Rover(position));

        public static Either<string, MissionControl> FromPlateau(Plateau plateau) => new MissionControl(plateau);

        public string Description()
        {
            var roversStrings = Rovers.Select(rover => rover.Description());
            var roversString = String.Join('\n', roversStrings);
            return $"Mission control status:\n" +
                $"\t- Plateau size: ({Plateau.MaxX}, {Plateau.MaxY})\n" +
                $"\t- Rovers:\n" + roversString;
        }

        public Either<string, Rover> GetRoverById(int roverId) =>
            Rovers.Where(rover => rover.Id == roverId).ToList().Count switch
            {
                0 => Left(Messages.RoverDoesNotExist(roverId)),
                1 => Right(Rovers.Where(rover => rover.Id == roverId).First()),
                _ => Left(Messages.MoreThanOneRoverWithId(roverId))
            };

        public Either<string, Rover> GetFirstRover()
        {
            var firstRover = Rovers.FirstOrDefault();
            return firstRover switch
            {
                null => Left("Mission control is not in contact with any rovers."),
                _ => Right(firstRover),
            };
        }

        public static Either<string, MissionControl> MoveRover(MissionControl mc, int roverId) =>
            from rover in mc.GetRoverById(roverId)
            from updatedMC in MoveRover(mc, rover)
            select updatedMC;

        public static Either<string, MissionControl> MoveRover(MissionControl mc, Rover rover) {
            switch (rover.Position)
            {
                case RoverPosition(_, 0, Direction.S):
                    return Left(Messages.CannotMoveRoverOffMap(rover.Id));

                case RoverPosition(_, _, Direction.S):
                    return rover.UpdateY(rover.Position.Y - 1).Bind(mc.UpdateRover);

                case RoverPosition(0, _, Direction.W):
                    return Left(Messages.CannotMoveRoverOffMap(rover.Id));

                case RoverPosition(_, _, Direction.W):
                    return rover.UpdateX(rover.Position.X - 1).Bind(mc.UpdateRover);

                case RoverPosition(_, _, Direction.N):
                    if (rover.Position.Y >= mc.Plateau.MaxY)
                    {
                        return Left(Messages.CannotMoveRoverOffMap(rover.Id));
                    }
                    return rover.UpdateY(rover.Position.Y + 1).Bind(mc.UpdateRover);

                case RoverPosition(_, _, Direction.E):
                    if (rover.Position.X >= mc.Plateau.MaxX)
                    {
                        return Left(Messages.CannotMoveRoverOffMap(rover.Id));
                    }
                    return rover.UpdateX(rover.Position.X + 1).Bind(mc.UpdateRover);

            }
            return Left(Messages.Unforeseen);
        }
        public static Either<string, MissionControl> RotateRover(MissionControl mc, int roverId, RotateInstruction rotation) =>
            from rover in mc.GetRoverById(roverId)
            from updatedMC in MissionControl.RotateRover(mc, rover, rotation)
            select updatedMC;

        public static Either<string, MissionControl> RotateRover(MissionControl mc, Rover rover, RotateInstruction rotation) =>
            from r in rover.Rotate(rotation)
            from updatedMC in mc.UpdateRover(r)
            select updatedMC;

        public static Either<string, MissionControl> DoInstructions(MissionControl mc, int roverId, Seq<Instruction> instructions)
        {
            if (instructions.Count == 0) return mc;

            var updatedMissionControl = instructions[0] switch
            {
                Instruction.Q => Left(Messages.QuitMessage),
                Instruction.M => from mc_ in MoveRover(mc, roverId)
                                 from mc__ in DoInstructions(mc_, roverId, instructions.Tail)
                                 select mc__,
                Instruction.L => from mc_ in RotateRover(mc, roverId, RotateInstruction.L)
                                 from mc__ in DoInstructions(mc_, roverId, instructions.Tail)
                                 select mc__,
                Instruction.R => from mc_ in RotateRover(mc, roverId, RotateInstruction.R)
                                 from mc__ in DoInstructions(mc, roverId, instructions.Tail)
                                 select mc__,
                _ => Left($"Could not move Rover {roverId}")
            };

            return updatedMissionControl;
            
        }

        public override string ToString()
        {
            return this.Description();
        }

        public Either<string, MissionControl> UpdateRover(Rover rover) => new MissionControl(Plateau, new Rover(rover.Position, rover.Id));

    }
}
