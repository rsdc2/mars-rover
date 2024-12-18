using MarsRover.Data;
using LanguageExt;
using static LanguageExt.Prelude; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MarsRover.Extensions;

internal static class CharExtensions
{
    internal static Either<string, Instruction> ToInstruction(this char c) => c switch
    {
        'M' => Right(Instruction.M),
        'R' => Right(Instruction.R),
        'L' => Right(Instruction.L),
        _ => Left($"{c} {Messages.ParseFailure}")
    };

    internal static Either<string, Direction> ToDirection(this char c) => c switch
    {
        'N' => Right(Direction.N),
        'E' => Right(Direction.E),
        'S' => Right(Direction.S),
        'W' => Right(Direction.W),
        _ => Left($"{c} {Messages.InvalidDirection(c)}")
    };

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
