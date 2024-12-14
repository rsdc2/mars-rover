using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LanguageExt;
using static LanguageExt.Prelude;

using FluentAssertions;

using MarsRover.Extensions;
using MarsRover.Data;
using LanguageExt.UnsafeValueAccess;

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
            Either<string, int> output = input.ToInt();

            // Assert
            var val = output.Value();
            val.Should().Be(expectedOutput);
        }

        [Test]
        [TestCase("")]
        [TestCase("hello")]
        [TestCase(".sdkjtw3")]
        public void StringToIntSadTest(string input)
        {
            // Act
            Either<string, int> output = input.ToInt();

            // Assert
            output.IsLeft.Should().BeTrue();
        }

        [Test]
        [TestCase("N", Direction.N)]
        [TestCase("E", Direction.E)]
        [TestCase("S", Direction.S)]
        [TestCase("W", Direction.W)]
        public void StringToDirectionHappyTest(string input, Direction expectedOutput)
        {
            // Act
            Either<string, Direction> output = input.ToDirection();

            // Assert
            output.Value().Should().Be(expectedOutput);
        }

        [Test]
        [TestCase("L")]
        [TestCase("T")]
        [TestCase(".sdkjtw3")]
        public void StringToDirectionSadTest(string input)
        {
            // Act
            Either<string, Direction> output = input.ToDirection();

            // Assert
            output.IsLeft.Should().BeTrue();
        }
    }
}
