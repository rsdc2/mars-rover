using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Types
{
    internal class Failure : ISuccessFailure
    {
        public string Message { get; set; }

        public Failure(string message)
        {
            Message = message;
        }
    }
}
