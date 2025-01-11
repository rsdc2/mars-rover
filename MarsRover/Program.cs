using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarsRover.Tests")]

namespace MarsRover;
using MarsRover.Input;
using MarsRover.Model;
using MarsRover.Data;
using MarsRover.UI;

internal class Program
{
    static void Happy()
    {
        var missionControl =
            from ps in ConsoleUI.GetPlateauSize()
            from plateau in Plateau.FromPlateauSize(ps)
            from mc in MissionControl.FromPlateau(plateau)
            from pos1 in ConsoleUI.GetInitialPosition()
            from pos2 in ConsoleUI.GetInitialPosition()
            from mc2 in mc.AddRover(pos1)
            from mc3 in mc.AddRover(pos2)
            select mc3;

        missionControl.IfRight(mc => Console.WriteLine(mc.Description()));

        var rover11 = missionControl.Bind(mc => mc.MoveRover(1));
        Console.WriteLine();
        missionControl.IfRight(mc => Console.WriteLine(mc.Description()));

        var rover2 = missionControl.Bind(mc => mc.RotateRover(2, RotateInstruction.L));
        Console.WriteLine();
        missionControl.IfRight(mc => Console.WriteLine(mc.Description()));
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
