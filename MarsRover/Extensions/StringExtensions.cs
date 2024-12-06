using MarsRover.Data;
using MarsRover.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Extensions;

internal static class StringExtensions
{
    internal static Either<int> ToInt(this string s)
    {
        try
        {
            return Either<int>.From(int.Parse(s));
        }
        catch (Exception ex)
        {
            return Either<int>.From(Messages.CannotParseStringToInteger(s, ex.Message));
        }
    }

    internal static Either<Direction> ToDirection(this string s) 
    {
        if (s.Length > 1) return Either<Direction>.From(Messages.InvalidDirection(s));    
        if (s.Length == 0) return Either<Direction>.From(Messages.InvalidDirection(s));

        return s[0].ToDirection();
    }
}
