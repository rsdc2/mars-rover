using MarsRover.Data;
using MarsRover.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Model
{
    internal class MissionControl
    {
        public List<Rover> Rovers { get; private set; } = [];
        public Plateau? Plateau { get; private set; }

        public MissionControl() { }

        /// <summary>
        /// Adds a rover to the inventory of rovers
        /// </summary>
        /// <param name="rover">A new Rover to add</param>
        /// <returns>An Either of the updated MissionControl</returns>
        public Either<MissionControl> AddRover(Rover rover)
        {
            Rovers.Add(rover);
            return Either<MissionControl>.From(this);
        }

        public Either<Plateau> AddPlateau(Plateau plateau)
        {
            return Either<Plateau>.From(Messages.CannotAddPlateau);
        }

        public Either<Rover> GetRoverById(int id) => Rovers.Where(rover => rover.Id == id).ToList().Count switch
        {
            0 => Either<Rover>.From(Messages.RoverDoesNotExist(id)),
            1 => Either<Rover>.From(Rovers.Where(rover => rover.Id == id).First()),
            _ => Either<Rover>.From(Messages.MoreThanOneRoverWithId(id))
        };

        public Either<Rover> RotateRover(int roverId, RotateInstruction rotation)
        {
            return GetRoverById(roverId).Bind(rover => rover.Rotate(rotation));
        }
    }
}
