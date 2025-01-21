using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Immutable;

using MarsRover.Data;
using LanguageExt;
using static LanguageExt.Prelude;
using MarsRover.Extensions;


namespace MarsRover.Input;

internal static partial class InputParser
{

    [GeneratedRegex(@"[LRMQlrmq]+")]
    private static partial Regex InstructionRegex();

    [GeneratedRegex(@"^([0-9]+?) ([0-9]+)$")]
    private static partial Regex PlateauSizeRegex();

    [GeneratedRegex(@"^([0-9]+?) ([0-9]+?) ([NESWnesw])$")]
    private static partial Regex PositionRegex();

    private static Either<string, (string, string)> GetPlateauSizeDataFromString(string plateauSize)
    {
        try
        {
            var groups = Regex
                .Matches(input: plateauSize, pattern: PlateauSizeRegex().ToString())[0]
                .Groups;

            var (x, y) = (groups[1].Value, groups[2].Value);
            return Right((x, y));
        }
        catch (Exception ex)
        {
            return Left(Messages.CannotGetPositionDataFromString(plateauSize, ex.Message));
        }
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

    public static bool IsValidInstruction(string instruction) =>
        InstructionRegex().IsMatch(instruction);
    
    public static bool IsValidPlateauDims(string dims) => 
        PlateauSizeRegex().IsMatch(dims);

    public static bool IsValidPosition(string position) =>
        PositionRegex().IsMatch(position);

    /// <summary>
    /// Trim the string and replace multiple spaces or tabs with a single space
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string NormalizeSpaces(string str) =>
         Regex.Replace(str.Trim(), @"[\s\t]+", " ");

    public static Either<string, Seq<Instruction>> ParseInstructions(string? instructionString)
    {
        if (instructionString == null) return Left(Messages.NoInstruction);
        instructionString = NormalizeSpaces(instructionString);
        if (instructionString == String.Empty) return Left(Messages.NoInstruction);
        return instructionString
            .Select(c => c.ToInstruction())
            .ToSeq()
            .Unwrap();
    }

    /// <summary>
    /// Return a PlateauSize object from a string containing the dimensions of the plateau
    /// </summary>
    /// <param name="dims">A string provided in the format "n+ n+"</param>
    /// <returns></returns>
    public static Either<string, PlateauSize> ParsePlateauSize(string? dims)
    {
        if (dims == null) return Left(Messages.EmptyInput);

        dims = NormalizeSpaces(dims);

        if (dims == String.Empty) 
            return Left(Messages.EmptyInput);

        else if (!(IsValidPlateauDims(dims)))
            return Left(Messages.InvalidDimensions(dims));

        return from xy in GetPlateauSizeDataFromString(dims)
               from x in xy.Item1.ToCoordinate()
               from y in xy.Item2.ToCoordinate()
               select PlateauSize.From(x, y);
    }

    public static Either<string, RoverPosition> ParsePosition(string? position)
    {
        if (position == null) return Left(Messages.NoPosition);

        position = NormalizeSpaces(position);

        if (position == String.Empty)
            return Left(Messages.NoPosition);
        else if (!(IsValidPosition(position)))
            return Left(Messages.InvalidPosition(position));

        return from triple in GetPositionDataFromString(position)
               from x in triple.Item1.ToCoordinate()
               from y in triple.Item2.ToCoordinate()
               from d in triple.Item3.ToDirection()
               select RoverPosition.From(x, y, d);
    }




}
