using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Types
{
    internal class Success<T> : ISuccessFailure
    {
        public string Message { get; set; } = string.Empty;
        public T Result { get; private set; }

        public Success(T result)
        {
            Result = result;
        }

        public Success(T result, string message) 
        {
            Message = message;
            Result = result;
        }
    }
}
