using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Types
{
    internal class Success<T> : IResult<T>
    {
        public string Message { get; } = string.Empty;
        public T Value { get; }

        public Success(T result)
        {
            Value = result;
        }

        public Success(T result, string message) 
        {
            Message = message;
            Value = result;
        }
    }
}
