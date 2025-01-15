using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LanguageExt;
using static LanguageExt.Prelude;

using MarsRover.Extensions;
using MarsRover.Data;
using LanguageExt.UnsafeValueAccess;
using NuGet.Frameworks;

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
            var val = output.Value();

            // Assert
            Assert.That(val, Is.EqualTo(expectedOutput));
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
            Assert.That(output.IsLeft, Is.True);
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
            Assert.That(output, Is.EqualTo(expectedOutput));
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
            Assert.That(output.IsLeft, Is.True);
        }
    }
}
