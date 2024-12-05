using FluentAssertions;
using MarsRover;
using MarsRover.Types;

namespace MarsRover.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Description("Test that creates a Success Either from static method")]
        public void CreateSuccessFromStaticMethod()
        {
            // Arrange
            int result = 3;
            
            // Act
            var either = Either<int>.FromSuccess(result, "Operation successful");

            // Assert
            Assert.That(either.Value is Success<int>);
        }

        [Test, Description("Test that creates a Failure Either from static method")]
        public void CreateFailureFromStaticMethod()
        {
            // Arrange

            // Act
            var either = Either<int>.FromFailure("Operation successful");

            // Assert
            Assert.That(either.Value is Failure);
        }
    }
}