using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Types
{
    internal class Either<T>
    {

        public ISuccessFailure Value {  get; set; }

        public Either(Success<T> success) 
        {
            Value = success;
        }

        public Either(Failure failure)
        {
            Value = failure;
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


    }
}
