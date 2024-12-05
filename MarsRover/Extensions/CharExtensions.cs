using MarsRover.Data;
using MarsRover.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Extensions;

internal static class CharExtensions
{
    internal static Either<Instruction> ToInstruction(this char c) => c switch
    {
        'M' => Either<Instruction>.From(Instruction.M),
        'R' => Either<Instruction>.From(Instruction.R),
        'L' => Either<Instruction>.From(Instruction.L),
        _ => Either<Instruction>.From($"{c} {Messages.ParseFailure}")
    };

    internal static Either<Direction> ToDirection(this char c) => c switch
    {
        'N' => Either<Direction>.From(Direction.N),
        'E' => Either<Direction>.From(Direction.E),
        'S' => Either<Direction>.From(Direction.S),
        'W' => Either<Direction>.From(Direction.W),
        _ => Either<Direction>.From($"{c} {Messages.InvalidDirection(c)}")
    };

    internal static Either<int> ToCoordinate(this char c) => c switch
    {
        '0' => Either<int>.From(0),
        '1' => Either<int>.From(1),
        '2' => Either<int>.From(2),
        '3' => Either<int>.From(3),
        '4' => Either<int>.From(4),
        _ => Either<int>.From($"{c} {Messages.InvalidCoordinate(c)}")
    };
}
