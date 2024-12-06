using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Types
{
    internal class Failure<T> : IResult<T>
    {
        public string Message { get; private set; }

        public T? Value { get; private set; } = default(T);

        public Failure(string message)
        {
            Message = message;
        }
    }
}
