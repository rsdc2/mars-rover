using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarsRover.Tests")]

namespace MarsRover;
using MarsRover.Input;
using MarsRover.Model;
using MarsRover.Types;
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
            .Bind(control => initialPosition2.Bind(pos => control.AddRover(pos)))
            .Result;

        Console.WriteLine(missionControl.Description());

        missionControl.MoveRover(1);
        Console.WriteLine();
        Console.WriteLine(missionControl.Description());

        missionControl.RotateRover(2, RotateInstruction.L);
        Console.WriteLine();
        Console.WriteLine(missionControl.Description());
    }

    static void Sad()
    {
        string plateauSizeInput = "5 5";
        string initialPosition1Input = "5 5 N";

        var plateauSize = InputParser.ParsePlateauSize(plateauSizeInput);
        var initialPosition1 = InputParser.ParsePosition(initialPosition1Input);

        //var missionControl = plateauSize.Fmap(size => new Plateau(size));

        var missionControl = plateauSize
            .Bind(Plateau.FromPlateauSize)
            .Bind(MissionControl.FromPlateau)
            .Bind(control => initialPosition1.Bind(pos => control.AddRover(pos)))
            .Result;

        Console.WriteLine(missionControl.Description());

        Console.WriteLine(missionControl.Description());

        Either<Rover> result = missionControl.MoveRover(1);
        Console.WriteLine(result.Message);
    }

    static void Main(string[] args)
    {
        //Happy();
        Sad();
    }
}
