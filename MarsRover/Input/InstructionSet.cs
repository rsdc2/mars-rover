using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarsRover.Data;
using LanguageExt;
using static LanguageExt.Prelude;
using System.Collections.Immutable;

namespace MarsRover.Input
{
    internal class InstructionSet : IEnumerable<Instruction>
    {
        private ImmutableList<Instruction> Instructions { get; set; }

        public InstructionSet(ImmutableList<Instruction> instructions) 
        {
            Instructions = instructions;
        }

        public static InstructionSet FromList(List<Instruction> instructions)
        {
            return new InstructionSet(instructions.ToImmutableList());    
        }

        public static InstructionSet FromImmutableList(ImmutableList<Instruction> instructions)
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
