﻿using FluentAssertions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarsRover.Input;
using MarsRover.Types;
using MarsRover.Data;

namespace MarsRover.Tests.Input;

internal class InputParserTests
{
    [Test]
    public void EmptyInputReturnsFailure()
    {
        // Arrange
        string instruction = "";

        // Act
        var result = InputParser.ParseInstruction(instruction);

        // Assert
        Assert.That(result.Message == Messages.NoInstruction);
    }

    [Test]
    public void InvalidCoordinatesReturnsFailureWithMessage()
    {
        // Arrange
        var positionString = "5 7 N";

        // Act
        var position = InputParser.ParsePosition(positionString);

        // Assert
        Assert.That(position.Message == Messages.InvalidPosition(positionString));
    }

    [Test]
    public void InvalidDirectionReturnsFailureWithMessage()
    {
        // Arrange
        var positionString = "1 2 L";

        // Act
        var position = InputParser.ParsePosition(positionString);

        // Assert
        Assert.That(position.Message == Messages.InvalidPosition(positionString));
    }

    [Test]
    public void ValidPositionReturnsSuccessWithPosition()
    {
        // Arrange
        var positionString = "1 2 N";

        // Act
        var position = (Success<Position>)InputParser.ParsePosition(positionString).Value;

        // Assert
        Assert.That(position.Result is Position);
    }


    [Test]
    public void InvalidInstructionsReturnsMessageWithFailureDescriptions()
    {
        // Arrange
        string instructions = "TDR";

        // Act
        var result = InputParser.ParseInstruction(instructions);

        // Assert
        result.Value.Message.Should().Be($"{Messages.CommandsNotCarriedOut}" +
            $":\nT {Messages.ParseFailure}\nD {Messages.ParseFailure}");
    }

    [Test]
    public void ValidInputReturnsSuccess()
    {
        // Arrange
        string instruction = "MLR";

        // Act
        var result = InputParser.ParseInstruction(instruction);

        // Assert
        Assert.That(result.Value is Success<InstructionSet>);
    }
}
