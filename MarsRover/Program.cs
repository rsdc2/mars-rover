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
    static void Happy()
    {

        var mc__ =  from mc in ConsoleUI.GetInitialSetup()
                    from mc_ in ConsoleUI.HandleUserInstructions(mc, "Start")
                    select mc_;
    }

    static void Main(string[] args)
    {
        Happy();
        //Sad();
    }
}
