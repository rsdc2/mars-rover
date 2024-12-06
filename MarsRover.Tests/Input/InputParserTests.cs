using FluentAssertions;

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

    [TestCase("1 3 ")]
    [TestCase ("5 7")]
    [TestCase ("10 5")]
    public void InvalidPlateauSizeReturnsFailureWithMessage(string sizeString)
    {
        // Act
        var size = InputParser.ParsePlateauDims(sizeString);

        // Assert
        size.Message.Should().Be(Messages.InvalidDimensions(sizeString));
    }


    public static IEnumerable<TestCaseData> TestPlateauSizes
    {
        get
        {
            yield return new TestCaseData("1 3", PlateauSize.From(1, 3));
            yield return new TestCaseData("3 4", PlateauSize.From(3, 4));
            yield return new TestCaseData("2 3", PlateauSize.From(2, 3));
        }
    }

    [TestCaseSource(nameof(TestPlateauSizes))]
    public void ValidPlateauSizeReturnsSuccessWithPlateauSize(
        string sizeString,
        PlateauSize expectedPlateauSize
    )
    {
        // Act
        var sizeResult = (Success<PlateauSize>)InputParser.ParsePlateauDims(sizeString).Value;
        var size = sizeResult.Value;

        // Assert
        size.x.Should().Be(expectedPlateauSize.x);
        size.y.Should().Be(expectedPlateauSize.y);
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
        var position = (Success<RoverPosition>)InputParser.ParsePosition(positionString).Value;

        // Assert
        Assert.That(position.Value is RoverPosition);
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
