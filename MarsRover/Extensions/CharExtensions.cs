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
}
