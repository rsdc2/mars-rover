using FluentAssertions;
using MarsRover.Data;
using MarsRover.Input;
using MarsRover.Model;
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
            var newDirection = rover.Rotate(rotation);

            // Assert
            newDirection.Should().Be(finalDirection);
        }

    }
}
