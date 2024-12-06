using FluentAssertions;
using MarsRover.Data;
using MarsRover.Input;
using MarsRover.Model;
using MarsRover.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Tests.Model
{
    internal class RoverTests
    {

        [TestCase(Direction.W, RotateInstruction.L, Direction.S)]
        [TestCase(Direction.S, RotateInstruction.R, Direction.W)]
        [TestCase(Direction.N, RotateInstruction.L, Direction.W)]
        [TestCase(Direction.E, RotateInstruction.R, Direction.S)]
        public void RoverRotationTests(
            Direction initialDirection, 
            RotateInstruction rotation,
            Direction finalDirection
        )
        {
            // Arrange 
            var position = new RoverPosition(1, 2, initialDirection);
            var rover = new Rover(position);

            // Act
            var newDirection = (Success<Direction>)rover.Rotate(rotation).Value;

            // Assert
            newDirection.Value.Should().Be(finalDirection);
        }

        [Test, Description("Test that can move the rover one position forwards in the direction that it is facing")]
        [TestCase(1, 1, Direction.N, 1, 2)]
        [TestCase(1, 1, Direction.W, 0, 1)]
        [TestCase(3, 4, Direction.S, 3, 3)]
        [TestCase(2, 3, Direction.E, 3, 3)]
        public void RoverMoveSuccessfullyTests(
            int initialX,
            int initialY,
            Direction initialDirection,
            int expectedX,
            int expectedY
        )
        {
            // Arrange 
            var position = new RoverPosition(initialX, initialY, initialDirection);
            var rover = new Rover(position);

            // Act
            var newPosition = rover.Move();

            // Assert
            newPosition.Result.X.Should().Be(expectedX);
            newPosition.Result.Y.Should().Be(expectedY);
        }
    

        [Test, Description("Test that returns failure when cannot move")]
        [TestCase(0, 0, Direction.S)]
        [TestCase(0, 0, Direction.W)]
        public void RoverMoveFailureTests(
            int initialX,
            int initialY,
            Direction initialDirection
        )
        {
            // Arrange 
            var position = new RoverPosition(initialX, initialY, initialDirection);
            var rover = new Rover(position);

            // Act
            var newPosition = rover.Move();

            // Assert
            Assert.That(newPosition.Value is Failure<Rover>);
        }
    }

}
