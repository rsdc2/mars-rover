using MarsRover.Data;
using MarsRover.Model;
using MarsRover.Types;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Tests.Model
{
    internal class MissionControlTests
    {
        [TestCase(1, 1, Direction.N, 1, 2, Direction.N)]
        [TestCase(1, 1, Direction.N, 1, 2, Direction.N)]
        [TestCase(1, 1, Direction.N, 1, 2, Direction.N)]
        [TestCase(1, 1, Direction.N, 1, 2, Direction.N)]
        [TestCase(1, 1, Direction.N, 1, 2, Direction.N)]
        public void RotateRoverTest(
            int initialX,
            int initialY,
            Direction initialDirection,
            int expectedX,
            int expectedY,
            Direction expectedDirection
        )
        {
            // Arrange 
            var position = new RoverPosition(initialX, initialY, initialDirection);
            var rover = new Rover(position);
            var missionControl = new MissionControl();
            missionControl.AddRover(rover);

            // Act
            var rotatedRover = (Success<Rover>)missionControl.RotateRover(1, RotateInstruction.R).Value;

            // Assert
            rotatedRover.Value.Direction.Should().Be(expectedDirection);
        }

    }
}
