using LanguageExt;
using MarsRover.Data;
using MarsRover.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Extensions.LanguageExt
{
    internal static class EitherExtensions
    {
        internal static void ToConsole<T>(this Either<string, T> either)
        {
            either
            .Right(t => Console.WriteLine(t))
            .Left(s => Console.WriteLine(s));
        }

    }
}
