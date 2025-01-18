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
                    from mc_ in ConsoleUI.HandleUserInstructions(mc)
                    select mc_;
    }

    static void Sad()
    {
        string plateauSizeInput = "5 5";
        string initialPosition1Input = "5 5 N";

        var plateauSize = InputParser.ParsePlateauSize(plateauSizeInput);
        var initialPosition1 = InputParser.ParsePosition(initialPosition1Input);

        var missionControl = plateauSize
            .Bind(Plateau.FromPlateauSize)
            .Bind(MissionControl.FromPlateau)
            .Bind(control => initialPosition1.Bind(pos => control.AddRover(pos)));

        missionControl.IfRight(mc => Console.WriteLine(mc.Description()));

        var result = missionControl.Bind(mc => mc.MoveRover(1));
        result.IfLeft(Console.WriteLine);
    }

    static void Main(string[] args)
    {
        Happy();
        //Sad();
    }
}
