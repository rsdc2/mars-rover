using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarsRover.Data;
using MarsRover.Types;

namespace MarsRover.Input
{
    internal class InstructionSet : IEnumerable<Instruction>
    {
        private List<Instruction> Instructions { get; set; }

        public InstructionSet(List<Instruction> instructions) 
        {
            Instructions = instructions;
        }

        public static InstructionSet FromList(List<Instruction> instructions)
        {
            return new InstructionSet(instructions);    
        }

        public IEnumerator<Instruction> GetEnumerator()
        {
            return ((IEnumerable<Instruction>)Instructions).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Instructions).GetEnumerator();
        }

    }
}
