using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarsRover.Input;
using MarsRover.Types;
using MarsRover.Data;

namespace MarsRover.Tests.Input;

internal class InstructionSetTests
{
    [Test]
    public void CreateInstructionSet()
    {
        // Arrange
        var instructions = new InstructionSet([Instruction.L, Instruction.R]);

        // Act

        // Assert
        Assert.That(instructions.Count() == 2);
    }

    [Test]
    public void CanIterateThroughInstructionSetTest()
    {
        // Arrange
        var instructions = new InstructionSet([Instruction.L, Instruction.R]);
        List<Instruction> instructionList = [];

        // Act
        foreach (var instruction in instructions)
        {
            instructionList.Add(instruction);
        }

        // Assert
        Assert.That(instructionList.Count == 2);
    }

}
