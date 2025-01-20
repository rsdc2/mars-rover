using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarsRover.Tests")]

namespace MarsRover;
using MarsRover.Input;
using MarsRover.Model;
using MarsRover.Data;
using MarsRover.UI;
using MarsRover.Extensions.LanguageExt;

internal class Program
{
    static void Main(string[] args)
    {
        var mc__ = from mc in ConsoleUI.GetInitialSetup()
                   from mc_ in ConsoleUI.HandleUserInstructions(mc, mc.Description())
                   select mc_;
    }
}
