using MarsRover.Data;
using LanguageExt;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Input;
using System.Runtime.CompilerServices;

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
            Rovers = new List<Rover> { rover };
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

        public MissionControl DeepCopy() => new MissionControl(this.Plateau, this.Rovers[0].Copy());

        public static Either<string, MissionControl> FromPlateau(Plateau plateau) => new MissionControl(plateau);

        public string Description()
        {
            var roversStrings = Rovers.Select(rover => rover.Description());
            var roversString = String.Join('\n', roversStrings);
            return $"Mission control status:\n" +
                $"\t- Plateau size: ({Plateau.MaxX}, {Plateau.MaxY})\n" +
                $"\t- {roversString}";
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

        public Either<string, MissionControl> MoveRover(int roverId) => 
            MoveRover(this, roverId);

        public static Either<string, MissionControl> MoveRover(MissionControl mc, int roverId) =>
            from rover in mc.GetRoverById(roverId)
            from updatedMc in MoveRover(mc, rover)
            select updatedMc;

        public static Either<string, MissionControl> MoveRover(MissionControl mc, Rover rover) {
            switch (rover.Position)
            {
                case RoverPosition(_, 0, Direction.S):
                    return Left(Messages.CannotMoveRoverToPositionOffMap(rover.Id, rover.Position with { Y = -1 }));

                case RoverPosition(_, _, Direction.S):
                    return rover.UpdateY(rover.Position.Y - 1).Bind(mc.UpdateRover);

                case RoverPosition(0, _, Direction.W):
                    return Left(Messages.CannotMoveRoverToPositionOffMap(rover.Id, rover.Position with { X = -1 }));

                case RoverPosition(_, _, Direction.W):
                    return rover.UpdateX(rover.Position.X - 1).Bind(mc.UpdateRover);

                case RoverPosition(_, _, Direction.N):
                    if (rover.Position.Y >= mc.Plateau.MaxY)
                    {
                        return Left(Messages.CannotMoveRoverToPositionOffMap(rover.Id, rover.Position with { Y = rover.Position.Y + 1}));
                    }
                    return rover.UpdateY(rover.Position.Y + 1).Bind(mc.UpdateRover);

                case RoverPosition(_, _, Direction.E):
                    if (rover.Position.X >= mc.Plateau.MaxX)
                    {
                        return Left(Messages.CannotMoveRoverToPositionOffMap(rover.Id, rover.Position with { Y = rover.Position.X + 1 }));
                    }
                    return rover.UpdateX(rover.Position.X + 1).Bind(mc.UpdateRover);
            }
            return Left(Messages.Unforeseen);
        }

        public Either<string, MissionControl> RotateRover(int roverId, RotateInstruction rotation) =>
            RotateRover(this, roverId, rotation);

        public static Either<string, MissionControl> RotateRover(MissionControl mc, int roverId, RotateInstruction rotation) =>
            from rover in mc.GetRoverById(roverId)
            from updatedMc in RotateRover(mc, rover, rotation)
            select updatedMc;

        public static Either<string, MissionControl> RotateRover(MissionControl mc, Rover rover, RotateInstruction rotation) =>
            from r in rover.Rotate(rotation)
            from updatedMc in mc.UpdateRover(r)
            select updatedMc;

        public Either<string, MissionControl> DoInstructions(int roverId, Seq<Instruction> instructions)
        {
            return DoInstructions(this, roverId, instructions);
        }

        public static Either<string, MissionControl> DoInstructions(MissionControl mc, int roverId, Seq<Instruction> instructions)
        {
            if (instructions.Count == 0) return mc;

            var updatedMissionControl = instructions[0] switch
            {
                Instruction.Q => Left(Messages.QuitMessage),
                Instruction.M => from updatedMc in MoveRover(mc, roverId)
                                 from finalMc in DoInstructions(updatedMc, roverId, instructions.Tail)
                                 select finalMc,
                Instruction.L => from updatedMc in RotateRover(mc, roverId, RotateInstruction.L)
                                 from finalMc in DoInstructions(updatedMc, roverId, instructions.Tail)
                                 select finalMc,
                Instruction.R => from updatedMc in RotateRover(mc, roverId, RotateInstruction.R)
                                 from finalMc in DoInstructions(updatedMc, roverId, instructions.Tail)
                                 select finalMc,
                _ => Left($"Could not move Rover {roverId}")
            };

            return updatedMissionControl;
        }

        public override string ToString() => Description();

        public Either<string, MissionControl> UpdateRover(Rover rover) => 
            new MissionControl(Plateau, new Rover(rover.Position, rover.Id));
    }
}
