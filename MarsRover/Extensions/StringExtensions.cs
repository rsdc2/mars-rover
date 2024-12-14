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
        <= 0 => Left(Messages.InvalidDirection(s)),
        _ => 
        if (s.Length > 1) return ;    
        if (s.Length == 0) return );

        return s[0].ToDirection();
    }

    internal static Either<string, int> ToCoordinate(this string s)
    {

    }
}
