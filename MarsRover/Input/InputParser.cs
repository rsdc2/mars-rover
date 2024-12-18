using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using MarsRover.Data;
using LanguageExt;
using static LanguageExt.Prelude;
using MarsRover.Extensions;
using System.ComponentModel;
using System.Collections.Immutable;

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

    public static Either<string, Seq<Instruction>> ParseInstructions(string instructionString)
    {
        if (instructionString == String.Empty) 
            return Left(Messages.NoInstruction);

        return instructionString
            .Select(c => c.ToInstruction())
            .ToSeq()
            .Unwrap();
    }

    private static Either<string, (string, string)> GetPlateauSizeDataFromString(string plateauSize)
    {
        try
        {
            var groups = Regex
                .Matches(input: plateauSize, pattern: PlateauSizeRegex().ToString())[0].Groups;

            var x = groups[1].Value;
            var y = groups[2].Value;

            return Right((x, y));
        }
        catch (Exception ex)
        {
            return Left(Messages.CannotGetPositionDataFromString(plateauSize, ex.Message));
        }
    }

    public static Either<string, PlateauSize> ParsePlateauSize(string dims)
    {
        if (dims == String.Empty)
        {
            return Left(Messages.EmptyInput);
        }
        else if (!(IsValidPlateauDims(dims)))
        {
            return Left(Messages.InvalidDimensions(dims));
        }

        var stringPlateauSizeData = GetPlateauSizeDataFromString(dims);
        var x = stringPlateauSizeData.Bind<((string x, string y) => Right(x.ToCoordinate(), y.ToCoordinate()));
        }
        //    Left: error => Left<string, PlateauSize>(error),
        //    Right: size => Right(size)
        //)
        //);
    }

    private static Either<(string, string, string)> GetPositionDataFromString(string position)
    {
        try
        {
            var groups = Regex
                .Matches(input: position, pattern: PositionRegex().ToString())[0].Groups;

            var x = groups[1].Value;
            var y = groups[2].Value;
            var direction = groups[3].Value;

            return Either<(string, string, string)>.From((x, y, direction));
        }
        catch (Exception ex)
        {
            return Either<(string, string, string)>
               .From(Messages.CannotGetPositionDataFromString(position, ex.Message));
        }
    }

    public static Either<RoverPosition> ParsePosition(string position)
    {
        if (position == String.Empty)
        {
            return Either<RoverPosition>.From(Messages.NoPosition);
        }
        else if (!(IsValidPosition(position)))
            return Either<RoverPosition>.From(Messages.InvalidPosition(position));

        var stringPositionData = GetPositionDataFromString(position);
        if (stringPositionData.IsFailure) 
            return Either<RoverPosition>.From(stringPositionData.Message);

        var (xStr, yStr, dStr) = stringPositionData.Result;
        Either<int> x = xStr.ToInt();
        Either<int> y = yStr.ToInt();
        Either<Direction> d = dStr.ToDirection();

        if (x.Value is Success<int> X && y.Value is Success<int> Y && d.Value is Success<Direction> D)
        {
            return Either<RoverPosition>.From(new RoverPosition(X.Value, Y.Value, D.Value));
        }
        return Either<RoverPosition>.From(Messages.InvalidPosition(position));
    }

    [GeneratedRegex(@"[LRM]+")]
    private static partial Regex InstructionRegex();

    [GeneratedRegex(@"^([0-9]+?) ([0-9]+)$")]
    private static partial Regex PlateauSizeRegex();

    [GeneratedRegex(@"^([0-9]+?) ([0-9]+?) ([NESW])$")]
    private static partial Regex PositionRegex();
}
