using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using MarsRover.Data;
using MarsRover.Types;
using MarsRover.Extensions;

namespace MarsRover.Input;

internal static partial class InputParser
{
    public static bool IsValidInstruction(string instruction)
    {
        var regex = InstructionRegex();
        return regex.IsMatch(instruction);
    }

    public static bool IsValidPlateauDims(string dims)
    {
        var regex = PlateauSizeRegex();
        return regex.IsMatch(dims);
    }

    public static bool IsValidPosition(string position)
    {
        var regex = PositionRegex();
        return regex.IsMatch(position);
    }

    public static Either<InstructionSet> ParseInstruction(string instructionString)
    {

        if (instructionString == String.Empty)
        {
            return Either<InstructionSet>.From(Messages.NoInstruction);
        }

        var instructions = instructionString.Select(c => c.ToInstruction());
        var unwrapped = Either<Instruction>.Unwrap(instructions);
        if (Either<Instruction>.Succeeded(instructions)) {
            return unwrapped.Fmap(InstructionSet.FromList);
        }

        var failureMessages = unwrapped.Message;

        return Either<InstructionSet>.From($"{Messages.CommandsNotCarriedOut}:\n{failureMessages}");
    }

    public static Either<PlateauSize> ParsePlateauDims(string dims)
    {
        if (dims == String.Empty)
        {
            return Either<PlateauSize>.From(Messages.EmptyInput);
        }
        else if (!(IsValidPlateauDims(dims)))
        {
            return Either<PlateauSize>.From(Messages.InvalidDimensions(dims));
        }

        var x = dims[0].ToCoordinate();
        var y = dims[2].ToCoordinate();

        if (x.Value is Success<int> X && y.Value is Success<int> Y)
        {
            return Either<PlateauSize>.From(new PlateauSize(X.Result, Y.Result));
        }
        return Either<PlateauSize>.From(Messages.InvalidDimensions(dims));
    }


    public static Either<RoverPosition> ParsePosition(string position)
    {
        if (position == String.Empty)
        {
            return Either<RoverPosition>.From(Messages.NoPosition);
        }
        else if (!(IsValidPosition(position))) 
        {
            return Either<RoverPosition>.From(Messages.InvalidPosition(position));
        }

        var x = position[0].ToCoordinate();
        var y = position[2].ToCoordinate();
        var direction = position[4].ToDirection();

        if (x.Value is Success<int> X && y.Value is Success<int> Y && direction.Value is Success<Direction> D)
        { 
            return Either<RoverPosition>.From(new RoverPosition(X.Result, Y.Result, D.Result));
        }
        return Either<RoverPosition>.From(Messages.InvalidPosition(position));
    }

    [GeneratedRegex(@"[LRM]+")]
    private static partial Regex InstructionRegex();

    [GeneratedRegex(@"^[0-5] [0-5]$")]
    private static partial Regex PlateauSizeRegex();

    [GeneratedRegex(@"^[0-5] [0-5] [NESW]$")]
    private static partial Regex PositionRegex();
}
