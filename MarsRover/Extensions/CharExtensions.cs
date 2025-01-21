using MarsRover.Data;
using LanguageExt;
using static LanguageExt.Prelude; 
using System.Text.RegularExpressions;
using System.Diagnostics.Contracts;

namespace MarsRover.Extensions;

internal static class CharExtensions
{
    [Pure]
    internal static Either<string, Instruction> ToInstruction(this char c) => c switch
    {
        'M' or 'm' => Right(Instruction.M),
        'R' or 'r' => Right(Instruction.R),
        'L' or 'l' => Right(Instruction.L),
        'Q' or 'q' => Right(Instruction.Q),
        _ => Left($"'{c}' {Messages.ParseFailure}")
    };

    [Pure]
    internal static Either<string, Direction> ToDirection(this char c) => c switch
    {
        'N' or 'n' => Right(Direction.N),
        'E' or 'e' => Right(Direction.E),
        'S' or 's' => Right(Direction.S),
        'W' or 'w' => Right(Direction.W),
        _ => Left($"{c} {Messages.InvalidDirection(c)}")
    };

    [Pure]
    internal static Either<string, int> ToCoordinateInt(this char c) => c switch
    {
        '0' => Right(0),
        '1' => Right(1),
        '2' => Right(2),
        '3' => Right(3),
        '4' => Right(4),
        '5' => Right(5),
        '6' => Right(6),
        '7' => Right(7),
        '8' => Right(8),
        '9' => Right(9),
        _ => Left($"{c} {Messages.InvalidCoordinate(c)}")
    };

    internal static Either<string, char> ToCoordinateChar(this char c) => 
        Regex.IsMatch($"{c}", @"^[0-9]+$") switch
    {
        true => Right(c),
        false => Left(Messages.InvalidCoordinate(c))
    };

    internal static Either<string, string> ToCoordinateStr(this char c) =>
        Regex.IsMatch($"{c}", @"^[0-9]+$") switch
        {
            true => Right($"{c}"),
            false => Left(Messages.InvalidCoordinate(c))
        };
}
