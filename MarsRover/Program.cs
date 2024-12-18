using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarsRover.Tests")]

namespace MarsRover;
using MarsRover.Input;
using MarsRover.Model;
using MarsRover.Data;

internal class Program
{
    static void Happy()
    {
        string plateauSizeInput = "5 5";
        string initialPosition1Input = "1 2 N";
        string initialPosition2Input = "3 3 E";

        var plateauSize = InputParser.ParsePlateauSize(plateauSizeInput);
        var initialPosition1 = InputParser.ParsePosition(initialPosition1Input);
        var initialPosition2 = InputParser.ParsePosition(initialPosition2Input);

        //var missionControl = plateauSize.Fmap(size => new Plateau(size));

        var missionControl = plateauSize
            .Bind(Plateau.FromPlateauSize)
            .Bind(MissionControl.FromPlateau)
            .Bind(control => initialPosition1.Bind(pos => control.AddRover(pos)))
            .Bind(control => initialPosition2.Bind(pos => control.AddRover(pos)));

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
        //Happy();
        Sad();
    }
}
