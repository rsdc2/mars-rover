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

        public static Either<string, Seq<Instruction>> GetUserInstructions()
        {
            Console.WriteLine(Messages.GetInstructions);
            string? instructionsInput = Console.ReadLine();
            var instructions = InputParser.ParseInstructions(instructionsInput);
            instructions.ToConsole();
            if (instructions.IsLeft) return GetUserInstructions();
            return instructions;
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

        public static Either<string, MissionControl> HandleUserInstructions(MissionControl missionControl)
        {
            var instructions = ConsoleUI.GetUserInstructions();

            var mc_ = from i in instructions
                    from rover in missionControl.GetFirstRover()
                    from mc in missionControl.DoInstructions(rover, i)
                    select mc;

            mc_.ToConsole();

            return mc_.Bind(HandleUserInstructions);
        }
    }
}
