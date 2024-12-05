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

        public ISuccessFailure Value { get; set; }
        public bool IsFailure { get => Value is Failure; }
        public bool IsSuccess { get => Value is Success<T>; }
        public string Message { get => Value.Message; }

        public Either(Success<T> success) 
        {
            Value = success;
        }

        public Either(Failure failure)
        {
            Value = failure;
        }

        public Either<U> Bind<U>(Func<T, Either<U>> f)
        {
            if (Value is Success<T> success)
            {
                var newValue = f(success.Result);
                return newValue;
            }

            return Either<U>.From(Value.Message);
        }

        public static List<Failure> Failures(IEnumerable<Either<T>> results)
        {
            List<Failure> failures = [];
            foreach (var either in results)
            {
                if (either.Value is Failure failure)
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
                var newValue = f(success.Result);
                return Either<U>.From(newValue);
            }

            return Either<U>.From(Value.Message);
        }

        public static Either<T> From(string message)
        {
            var failure = new Failure(message);
            return new Either<T>(failure);
        }

        public static Either<T> From(T result)
        {
            var success = new Success<T>(result);
            return new Either<T>(success);
        }

        public static Either<T> FromFailure(string message)
        {
            var failure = new Failure(message);
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
                var newResults = results.Select(result => ((Success<T>)result.Value).Result).ToList();
                return Either<List<T>>.From(newResults);
            }
            var messages = Failures(results).Select(failure => failure.Message);
            var message = String.Join("\n", messages);
            var failure = Either<List<T>>.From(message);
            return failure;
        }


    }
}
