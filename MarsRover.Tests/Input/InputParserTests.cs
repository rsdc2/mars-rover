using FluentAssertions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarsRover.Input;
using MarsRover.Types;
using MarsRover.Data;
using LanguageExt.UnsafeValueAccess;
using static LanguageExt.Prelude;

namespace MarsRover.Tests.Input;

internal class InputParserTests
{
    [Test]
    public void EmptyInputReturnsFailure()
    {
        // Arrange
        string instruction = "";

        // Act
        var result = InputParser.ParseInstructions(instruction);

        // Assert
        result.IfLeft(error => Assert.That(error == Messages.NoInstruction));
        result.IfRight(_ => Assert.That(false));
    }

    [TestCase("1 3 ")]
    [TestCase ("asd5 7")]
    [TestCase ("10ga5")]
    public void InvalidPlateauSizeReturnsFailureWithMessage(string sizeString)
    {
        // Act
        var size = InputParser.ParsePlateauSize(sizeString);

        // Assert
        size.IfLeft(error => error.Should().Be(Messages.InvalidDimensions(sizeString)));
        size.IfRight(_ => Assert.That(false));
    }
    public static IEnumerable<TestCaseData> TestPlateauSizes
    {
        get
        {
            yield return new TestCaseData("1 3", PlateauSize.From(1, 3));
            yield return new TestCaseData("3 4", PlateauSize.From(3, 4));
            yield return new TestCaseData("2 3", PlateauSize.From(2, 3));
            yield return new TestCaseData("10 20", PlateauSize.From(10, 20));
            yield return new TestCaseData("22 30", PlateauSize.From(22, 30));
            yield return new TestCaseData("3 15", PlateauSize.From(3, 15));
        }
    }

    [TestCaseSource(nameof(TestPlateauSizes))]
    public void ValidPlateauSizeReturnsSuccessWithPlateauSize(
        string sizeString,
        PlateauSize expectedPlateauSize
    )
    {
        // Act
        var sizeResult = InputParser.ParsePlateauSize(sizeString);

        // Assert
        sizeResult.IfRight(size => size.X.Should().Be(expectedPlateauSize.X));
        sizeResult.IfRight(size => size.Y.Should().Be(expectedPlateauSize.Y));
        sizeResult.IfLeft(_ => Assert.That(false));
    }

    [Test]
    [TestCase("-1 2 N")]
    [TestCase("-5 2 E")]
    [TestCase("7 -3 S")]
    [TestCase("8 -9 W")]
    public void InvalidCoordinatesReturnsFailureWithMessage(string position)
    {
        // Act
        var parsedPosition = InputParser.ParsePosition(position);

        // Assert
        parsedPosition.IfLeft(error => error.Should().Be(Messages.InvalidPosition(position)));
        parsedPosition.IfRight(_ => Assert.That(false));
    }

    [Test]
    public void InvalidDirectionReturnsFailureWithMessage()
    {
        // Arrange
        var positionString = "1 2 L";

        // Act
        var position = InputParser.ParsePosition(positionString);

        // Assert
        Assert.That(position.IsLeft);
        position.IfLeft(error => error.Should().Be(Messages.InvalidPosition(positionString)));
    }

    [Test]
    [TestCase("1 2 N")]
    [TestCase("5 2 E")]
    [TestCase("7 3 S")]
    [TestCase("8 9 W")]
    public void ValidPositionReturnsSuccessWithPosition(string position)
    {
        // Act
        var parsedPosition = InputParser.ParsePosition(position);

        // Assert
        Assert.That(parsedPosition.IsRight);
    }


    [Test]
    public void InvalidInstructionsReturnsMessageWithFailureDescriptions()
    {
        // Arrange
        string instructions = "TDR";

        // Act
        var result = InputParser.ParseInstructions(instructions);

        // Assert
        Assert.That(result.IsLeft);
    }

    [Test]
    public void ValidInputReturnsSuccess()
    {
        // Arrange
        string instruction = "MLR";

        // Act
        var result = InputParser.ParseInstructions(instruction);

        // Assert
        Assert.That(result.IsRight);
    }
}
