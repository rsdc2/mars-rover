using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Types
{
    internal interface IResult<T>

    {
        public string Message { get; }

        public T Value { get; }
    }
}
