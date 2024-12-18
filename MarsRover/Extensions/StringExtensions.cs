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

internal static class StringExtensions
{
    internal static Either<string, int> ToInt(this string s)
    {
        try
        {
            return Right(int.Parse(s));
        }
        catch (Exception ex)
        {
            return Left(Messages.CannotParseStringToInteger(s, ex.Message));
        }
    }

    internal static Either<string, Direction> ToDirection(this string s) => s.Length switch
    {
        0    => Left(Messages.NoInstruction),
        1    => s[0].ToDirection(),
        _    => Left(Messages.InvalidDirection(s))
    };

    internal static Either<string, int> ToCoordinate(this string s) =>
        s.Aggregate(
            (Either<string, string>)Right(""), (acc, c) =>
                acc.Bind(coords => c.ToCoordinateStr().Bind<string>(coord => Right(coords + coord)))
        ).Bind(coords => coords.ToInt());
}
//return acc.Match<Either<string, string>>(
//    Left: error => Left(error),
//    Right: coordStr => Right(coordStr + coord)
//); 