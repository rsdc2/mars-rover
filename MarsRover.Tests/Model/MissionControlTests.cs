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
            Assert.That(updatedMissionControl.IsRight);
            updatedMissionControl.IfRight(mc => mc.Rovers.Count.Should().Be(1));
        }

        [Test, Description("Test that can add plateau successfully")]
        public void AddPlateauSuccess()
        {
            // Arrange 
            var position = new RoverPosition(1, 1, Direction.W);
            var plateau = Plateau.FromInts(5, 5);
            var missionControl = new MissionControl();

            // Act
            var updatedMissionControl = missionControl.AddPlateau(plateau);

            // Assert
            Assert.That(updatedMissionControl.IsRight);
            updatedMissionControl.IfRight(mc => mc.Plateau.Should().NotBeNull());
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
            Assert.That(foundRover.IsRight);
        }


        [Test, Description("Test that returns Failure if Rover does not exist")]
        public void FindRoverFailureTest()
        {
            // Arrange 
            var missionControl = new MissionControl();

            // Act
            var foundRover = missionControl.GetRoverById(1);

            // Assert
            Assert.That(foundRover.IsLeft);
        }

        [Test, Description("Test that mission control can move a rover sucessfully")]
        [TestCase(1, 1, Direction.N, 1, 2)]
        [TestCase(1, 1, Direction.W, 0, 1)]
        [TestCase(1, 1, Direction.S, 1, 0)]
        [TestCase(1, 1, Direction.E, 2, 1)]

        public void MoveRoverSucessTest(
            int initialX,
            int initialY,
            Direction initialDirection,
            int expectedX,
            int expectedY
        )
        {
            // Arrange 
            var rover = Rover.From(1, 1, initialDirection);
            var plateau = Plateau.FromInts(5, 5);
            var missionControl = new MissionControl();
            missionControl.AddPlateau(plateau);
            missionControl.AddRover(rover);

            // Act
            var rotatedRover = missionControl.MoveRover(Rover.RoverCount);

            // Assert
            rotatedRover.IsRight.Should().BeTrue();
            rotatedRover.IfRight(rover => rover.Position.X.Should().Be(expectedX));
            rotatedRover.IfRight(rover => rover.Position.Y.Should().Be(expectedY));
        }

        [Test, Description("Test that returns failure if moves to an impossible location")]
        [TestCase(0, 0, Direction.S, 5, 5)]
        [TestCase(0, 0, Direction.W, 5, 5)]
        [TestCase(1, 1, Direction.N, 1, 1)]
        [TestCase(3, 5, Direction.E, 3, 7)]

        public void MoveRoverFailureTest(
                    int initialX,
                    int initialY,
                    Direction initialDirection,
                    int plateauX,
                    int plateauY
                )
        {
            // Arrange 
            var rover = Rover.From(initialX, initialY, initialDirection);
            var plateau = Plateau.FromInts(plateauX, plateauY);
            var missionControl = new MissionControl();
            missionControl.AddPlateau(plateau);
            missionControl.AddRover(rover);

            // Act
            var movedRover = missionControl.MoveRover(Rover.RoverCount);

            // Assert
            movedRover.IsLeft.Should().BeTrue();
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
            Assert.That(rotatedRover.IsRight);
            rotatedRover.IfRight(rover => rover.Direction.Should().Be(expectedDirection));

        }

    }
}
