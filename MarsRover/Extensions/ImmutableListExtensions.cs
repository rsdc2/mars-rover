using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;

using static LanguageExt.Prelude; 

namespace MarsRover.Extensions
{
    internal static class ImmutableListExtensions
    {
        public static bool Succeeded<T>(this Seq<Either<string, T>> seq)
        {
            return seq.All(item => item.IsRight);
        }
        internal static Either<string, Seq<T>> Unwrap<T>(this Seq<Either<string, T>> seq)
        {
            if (Succeeded(seq))
            {
                return Right(seq.Map(right => right.RightToSeq()).Flatten());
            }

            return Left(String.Join('\n', seq.Map(left => left.LeftToSeq()).Flatten()));
        }

    }
}
