using MarsRover.Data;
using MarsRover.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LanguageExt;

namespace MarsRover.UI
{
    internal class ConsoleUI
    {
        public static Either<string, PlateauSize> GetPlateauSize()
        {
            Console.WriteLine(Messages.GetPlateauSize);
            var plateauSizeInput = Console.ReadLine();
            return InputParser.ParsePlateauSize(plateauSizeInput);
        }
        public static Either<string, Seq<Instruction>> GetUserInstructions()
        {
            Console.WriteLine(Messages.GetInstructions);
            var userInstructions = Console.ReadLine();
            return InputParser.ParseInstructions(userInstructions);
        }

        public static Either<string, RoverPosition> GetInitialPosition()
        {
            Console.WriteLine(Messages.GetInitialPosition);
            var initialPositionInput = Console.ReadLine();
            return InputParser.ParsePosition(initialPositionInput);
        }
    }
}
