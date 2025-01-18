using MarsRover.Data;
using MarsRover.Input;
using MarsRover.Extensions.LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LanguageExt;
using static LanguageExt.Prelude;

using MarsRover.Model;
using System.Reflection.Metadata.Ecma335;
using LanguageExt.UnsafeValueAccess;

namespace MarsRover.UI
{
    internal class ConsoleUI 
    {

        public static Either<string, MissionControl> GetInitialSetup()
        {
            return   from ps in GetPlateauSize()
                     from plateau in Plateau.FromPlateauSize(ps)
                     from mc in MissionControl.FromPlateau(plateau)
                     from pos1 in GetInitialPosition()
                     from mc_ in mc.AddRover(pos1)
                     select mc_;
        }

        public static Either<string, PlateauSize> GetPlateauSize()
        {
            Console.WriteLine(Messages.GetPlateauSize);
            string? plateauSizeInput = Console.ReadLine();
            var plateauSize = InputParser.ParsePlateauSize(plateauSizeInput);
            plateauSize.ToConsole();
            if (plateauSize.IsLeft) return GetPlateauSize();
            return plateauSize;
        }

        public static Either<string, Seq<Instruction>> GetUserInstructions(MissionControl mc, string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("Current status: \n" + mc.ToString() + "\n");
            Console.WriteLine(Messages.GetInstructions);
            string? instructionsInput = Console.ReadLine();
            var instructions = InputParser.ParseInstructions(instructionsInput);
            return instructions.Match
            (
                Right: instructions => instructions,
                Left: error => GetUserInstructions(mc, error)
            );
        }


        public static Either<string, RoverPosition> GetInitialPosition()
        {
            Console.WriteLine(Messages.GetInitialPosition);
            string? initialPositionInput = Console.ReadLine();
            var initialPosition = InputParser.ParsePosition(initialPositionInput);
            initialPosition.ToConsole();
            if (initialPosition.IsLeft) return GetInitialPosition();
            return initialPosition;
        }

        public static Either<string, MissionControl> HandleUserInstructions(MissionControl mc, string message)
        {
            var instructions = GetUserInstructions(mc, message);

            var updatedMc = from i in instructions
                    from rover in mc.GetFirstRover()
                    from mc_ in MissionControl.DoInstructions(mc, rover.Id, i)
                    select mc_;

            updatedMc.ToConsole();

            //return updatedMc.IsRight switch
            //{
            //    true => updatedMc.Bind(HandleUserInstructions),
            //    false => (updatedMc == Messages.QuitMessage) switch
            //    {
            //        true => updatedMc,
            //        false => HandleUserInstructions(mc)
            //    }
            //};

            return updatedMc.Match
            (
                Right: mc => HandleUserInstructions(mc, "Instruction successful"),
                Left: error => (updatedMc == Messages.QuitMessage) switch
                {
                    false => HandleUserInstructions(mc, error),
                    true => updatedMc
                }
            );
        }
    }
}
