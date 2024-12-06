using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarsRover.Tests")]

namespace MarsRover;
using MarsRover.Input;
using MarsRover.Model;
using MarsRover.Types;
using MarsRover.Data;

internal class Program
{
    static void Main(string[] args)
    {
        string plateauSizeInput = "5 5";
        string initialPosition1Input = "1 2 N";
        string instructions1Input = "LMLMLMLMM";
        string initialPosition2Input = "3 3 E";
        string instructions2Input = "MMRMMRMRRM";

        var plateauSize = InputParser.ParsePlateauSize(plateauSizeInput);
        var initialPosition1 = InputParser.ParsePosition(initialPosition1Input);
        var instructions1 = InputParser.ParseInstruction(instructions1Input);
        var initialPosition2 = InputParser.ParsePosition(initialPosition2Input);
        var instructions2 = InputParser.ParseInstruction(instructions2Input);

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
}
