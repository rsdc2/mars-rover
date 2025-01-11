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

    public static bool IsValidPlateauDims(string dims) => PlateauSizeRegex().IsMatch(dims);

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

    /// <summary>
    /// Return a PlateauSize object from a string containing the dimensions of the plateau
    /// </summary>
    /// <param name="dims">A string provided in the format "n+ n+"</param>
    /// <returns></returns>
    public static Either<string, PlateauSize> ParsePlateauSize(string? dims)
    {
        if (dims == String.Empty || dims == null) 
            return Left(Messages.EmptyInput);
        else if (!(IsValidPlateauDims(dims)))
            return Left(Messages.InvalidDimensions(dims));

        var stringPlateauSizeData = GetPlateauSizeDataFromString(dims);
        var x = stringPlateauSizeData.Bind(pair => pair.Item1.ToCoordinate());
        var y = stringPlateauSizeData.Bind(pair => pair.Item2.ToCoordinate());

        return x.Bind(x => y.Bind<(int, int)>(y => (x, y)))
                .Map(PlateauSize.From);
    }

    private static Either<string, (string, string, string)> GetPositionDataFromString(string position)
    {
        try
        {
            var groups = Regex
                .Matches(input: position, pattern: PositionRegex().ToString())[0].Groups;

            var x = groups[1].Value;
            var y = groups[2].Value;
            var direction = groups[3].Value;

            return Right((x, y, direction));
        }
        catch (Exception ex)
        {
            return Left(Messages.CannotGetPositionDataFromString(position, ex.Message));
        }
    }
    
    public static Either<string, RoverPosition> ParsePosition(string? position)
    {
        if (position == String.Empty || position == null)
            return Left(Messages.NoPosition);
        else if (!(IsValidPosition(position)))
            return Left(Messages.InvalidPosition(position));

        return from triple in GetPositionDataFromString(position)
               from x in triple.Item1.ToCoordinate()
               from y in triple.Item2.ToCoordinate()
               from d in triple.Item3.ToDirection()
               select RoverPosition.From(x, y, d);
    }

    [GeneratedRegex(@"[LRM]+")]
    private static partial Regex InstructionRegex();

    [GeneratedRegex(@"^([0-9]+?) ([0-9]+)$")]
    private static partial Regex PlateauSizeRegex();

    [GeneratedRegex(@"^([0-9]+?) ([0-9]+?) ([NESW])$")]
    private static partial Regex PositionRegex();
}
