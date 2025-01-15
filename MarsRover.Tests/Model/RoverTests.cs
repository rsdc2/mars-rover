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
            var roverWithNewDirection = rover.Rotate(rotation);

            // Assert
            Assert.That(roverWithNewDirection.IsRight);
            roverWithNewDirection.IfRight(rover => Assert.That(rover.Direction, Is.EqualTo(finalDirection)));
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
            Assert.That(newPosition.IsRight);
            newPosition.IfRight(pos => Assert.That(pos.X, Is.EqualTo(expectedX)));
            newPosition.IfRight(pos => Assert.That(pos.Y, Is.EqualTo(expectedY)));
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
            Assert.That(newPosition.IsLeft);
        }
    }

}
