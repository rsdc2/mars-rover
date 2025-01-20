﻿using MarsRover.Data;
using MarsRover.Input;
using MarsRover.Extensions.LanguageExt;
using MarsRover.Model;

using LanguageExt;
using static LanguageExt.Prelude;


namespace MarsRover.UI
{
    internal class ConsoleUI
    {

        public static Either<string, MissionControl> GetInitialSetup()
        {
            return from ps in GetPlateauSize(None)
                   from plateau in Plateau.FromPlateauSize(ps)
                   from mc in MissionControl.FromPlateau(plateau)
                   from pos1 in GetInitialPosition(ps, None)
                   from mc_ in mc.AddRover(pos1)
                   select mc_;
        }

        public static Either<string, PlateauSize> GetPlateauSize(Option<string> message)
        {
            Console.Clear();
            message.IfSome(msg => Console.WriteLine(msg + '\n'));
            Console.WriteLine(Messages.GetPlateauSize);
            string? plateauSizeInput = Console.ReadLine();
            var plateauSize = InputParser.ParsePlateauSize(plateauSizeInput);
            return plateauSize.Match(
                Right: ps => ps,
                Left: error => GetPlateauSize(error)
            );
        }

        public static Either<string, Seq<Instruction>> GetUserInstructions(MissionControl mc, Option<string> message)
        {
            Console.Clear();
            message.IfSome(msg => Console.WriteLine(msg + "\n"));
            Console.WriteLine(Messages.GetInstructions);
            string? instructionsInput = Console.ReadLine();
            var instructions = InputParser.ParseInstructions(instructionsInput);
            return instructions.Match
            (
                Right: instructions => instructions,
                Left: error => GetUserInstructions(mc, error + " Rover not moved.\n\n" + mc.Description())
            );
        }

        public static Either<string, RoverPosition> GetInitialPosition(PlateauSize plateauSize, Option<string> message)
        {
            Console.Clear();
            message.IfSome(msg => Console.WriteLine(msg + "\n"));
            Console.WriteLine(Messages.PlateauSize(plateauSize) + '\n');
            Console.WriteLine(Messages.GetInitialPosition);
            string? initialPositionInput = Console.ReadLine();
            var initialPosition = InputParser.ParsePosition(initialPositionInput);
            initialPosition.ToConsole();
            return initialPosition.Match
            (
                Right: initialPosition => initialPosition,
                Left: error => GetInitialPosition(plateauSize, error)
            );
        }

        public static Either<string, MissionControl> HandleUserInstructions(MissionControl mc, string message)
        {
            var instructionsEither = GetUserInstructions(mc, message);
            var roverInitialEither = mc.GetFirstRover();

            var updatedMcEither = from instructions in instructionsEither
                                  from rover in roverInitialEither
                                  from updatedMc in MissionControl.DoInstructions(mc, rover.Id, instructions)
                                  select updatedMc;

            return updatedMcEither.Match
            (
                Right: updatedMc => from instructions in instructionsEither
                             from roverInitial in roverInitialEither
                             from roverUpdated in updatedMc.GetFirstRover()
                             let message = Messages.MoveSuccessful(roverInitial.Position, roverUpdated.Position)
                             from finalMc in HandleUserInstructions(updatedMc, message + "\n\n" + updatedMc.Description())
                             select finalMc,

                Left: error => (updatedMcEither == Messages.QuitMessage) switch
                {
                    false => HandleUserInstructions(mc, error + "\n\n" + mc.ToString()),
                    true => updatedMcEither
                }
            );
        }
    }
}
