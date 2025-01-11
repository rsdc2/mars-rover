using MarsRover.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.UI
{
    internal class ConsoleUI
    {
        public static string? GetPlateauSize()
        {
            Console.WriteLine(Messages.GetPlateauSize);
            return Console.ReadLine();
        }
        public static string? GetUserInstructions()
        {
            Console.WriteLine(Messages.GetInstructions);
            return Console.ReadLine();
        }

        public static string? GetInitialPosition()
        {
            Console.WriteLine(Messages.GetInitialPosition);
            return Console.ReadLine();
        }
    }
}
