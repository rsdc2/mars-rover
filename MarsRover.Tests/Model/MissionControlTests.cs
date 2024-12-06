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

        [Test, Description("Test that can find rover with Id")]
        public void AddRoverSuccessTest()
        {
            // Arrange 
            var position = new RoverPosition(1, 1, Direction.W);
            var rover = new Rover(position);
            var missionControl = new MissionControl();

            // Act
            var updatedMissionControl = missionControl.AddRover(rover);

            // Assert
            updatedMissionControl.Value.Value.Rovers.Count.Should().Be(1);
            missionControl.Rovers.Count.Should().Be(1);
        }

        [Test, Description("Test that can add plateau successfully")]
        public void AddPlateauSuccess()
        {
            // Arrange 
            var position = new RoverPosition(1, 1, Direction.W);
            var rover = Plateau.From(5, 5);
            var missionControl = new MissionControl();

            // Act
            var updatedMissionControl = missionControl.AddPlateau(rover);

            // Assert
            updatedMissionControl.Result.Should().NotBeNull();
        }

        [Test, Description("Test that can find rover with Id")]
        public void FindRoverSuccessTest()
        {
            // Arrange 
            var position = new RoverPosition(1, 1, Direction.W);
            var rover = new Rover(position);
            var missionControl = new MissionControl();
            missionControl.AddRover(rover);

            // Act
            var foundRover = missionControl.GetRoverById(Rover.RoverCount);

            // Assert
            foundRover.IsSuccess.Should().BeTrue();
        }


        [Test, Description("Test that returns Failure if Rover does not exist")]
        public void FindRoverFailureTest()
        {
            // Arrange 
            var missionControl = new MissionControl();

            // Act
            var foundRover = missionControl.GetRoverById(1);

            // Assert
            foundRover.IsFailure.Should().BeTrue();
        }


        [Test, Description("Test that mission control can rotate a rover sucessfully")]
        [TestCase(Direction.E, RotateInstruction.L, Direction.N)]
        [TestCase(Direction.S, RotateInstruction.L, Direction.E)]
        [TestCase(Direction.W, RotateInstruction.L, Direction.S)]
        [TestCase(Direction.N, RotateInstruction.R, Direction.E)]
        [TestCase(Direction.E, RotateInstruction.R, Direction.S)]
        [TestCase(Direction.S, RotateInstruction.R, Direction.W)]
        [TestCase(Direction.W, RotateInstruction.R, Direction.N)]
        public void RotateRoverTest(
            Direction initialDirection,
            RotateInstruction rotation,
            Direction expectedDirection
        )
        {
            // Arrange 
            var rover = Rover.From(1, 1, initialDirection);
            var missionControl = new MissionControl();
            missionControl.AddRover(rover);

            // Act
            var rotatedRover = missionControl.RotateRover(Rover.RoverCount, rotation);

            // Assert
            rotatedRover.Value.Value.Direction.Should().Be(expectedDirection);
        }

    }
}
