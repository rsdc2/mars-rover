using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using MarsRover.Extensions;
using MarsRover.Types;
using MarsRover.Data;

namespace MarsRover.Tests.Extensions
{
    internal class StringExtensionsTests
    {
        [Test]
        [TestCase("123", 123)]
        [TestCase("235", 235)]

        public void StringToIntHappyTest(string input, int expectedOutput)
        {
            // Act
            Either<int> output = input.ToInt();

            // Assert
            output.Result.Should().Be(expectedOutput);
        }

        [Test]
        [TestCase("")]
        [TestCase("hello")]
        [TestCase(".sdkjtw3")]
        public void StringToIntSadTest(string input)
        {
            // Act
            Either<int> output = input.ToInt();

            // Assert
            output.IsFailure.Should().BeTrue();
        }

        [Test]
        [TestCase("N", Direction.N)]
        [TestCase("E", Direction.E)]
        [TestCase("S", Direction.S)]
        [TestCase("W", Direction.W)]
        public void StringToDirectionHappyTest(string input, Direction expectedOutput)
        {
            // Act
            Either<Direction> output = input.ToDirection();

            // Assert
            output.Result.Should().Be(expectedOutput);
        }

        [Test]
        [TestCase("L")]
        [TestCase("T")]
        [TestCase(".sdkjtw3")]
        public void StringToDirectionSadTest(string input)
        {
            // Act
            Either<Direction> output = input.ToDirection();

            // Assert
            output.IsFailure.Should().BeTrue();
        }
    }
}
