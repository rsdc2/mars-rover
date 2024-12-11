using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Types
{
    internal class Either<T>
    {
        public IResult<T> Value { get; set; }
        public bool IsFailure { get => Value is Failure<T>; }
        public bool IsSuccess { get => Value is Success<T>; }
        public string Message { get => Value.Message; }

        public T Result { get => Value.Value; }

        public Either(Success<T> success) 
        {
            Value = success;
        }

        public Either(Failure<T> failure)
        {
            Value = failure;
        }

        public Either<U> Bind<U>(Func<T, Either<U>> f)
        {
            if (Value is Success<T> success)
            {
                var newEither = f(success.Value);
                return newEither;
            }

            return Either<U>.From(Value.Message);
        }

        public static List<Failure<T>> Failures(IEnumerable<Either<T>> results)
        {
            List<Failure<T>> failures = [];
            foreach (var either in results)
            {
                if (either.Value is Failure<T> failure)
                {
                    failures.Add(failure);
                }
            }

            return failures;
        }

        public Either<U> Fmap<U>(Func<T, U> f)
        {
            if (Value is Success<T> success)
            {
                var newValue = f(success.Value);
                return Either<U>.From(newValue);
            }

            return Either<U>.From(Value.Message);
        }

        public static Either<T> From(string message)
        {
            var failure = new Failure<T>(message);
            return new Either<T>(failure);
        }

        public static Either<T> From(T result)
        {
            var success = new Success<T>(result);
            return new Either<T>(success);
        }

        public static Either<T> FromFailure(string message)
        {
            var failure = new Failure<T>(message);
            return new Either<T>(failure);
        }

        public static Either<T> FromSuccess(T result, string message)
        {
            var success = new Success<T>(result, message);
            return new Either<T>(success);
        }


        public static bool Succeeded(IEnumerable<Either<T>> results)
        {
            var succeeded = results.All(item => item.Value is Success<T>);
            return succeeded;
        }



        public static Either<List<T>> Unwrap(IEnumerable<Either<T>> results)
        {
            var resultsList = results.ToList();
            if (Succeeded(resultsList))
            {
                var newResults = results.Select(result => ((Success<T>)result.Value).Value).ToList();
                return Either<List<T>>.From(newResults);
            }
            var messages = Failures(results).Select(failure => failure.Message);
            var message = String.Join("\n", messages);
            var failure = Either<List<T>>.From(message);
            return failure;
        }


    }
}
