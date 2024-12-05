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
        /// <returns>An Either of a List of all Rover objects</returns>
        public Either<List<Rover>> AddRover(Rover rover)
        {
            return Either<List<Rover>>.From(Messages.CannotAddRover);
        }

        public Either<Plateau> AddPlateau(Plateau plateau)
        {
            return Either<Plateau>.From(Messages.CannotAddPlateau);
        }

        public Either<Rover> RotateRover(int roverId, RotateInstruction rotation)
        {
            return Either<Rover>.From(Messages.CannotRotateRover(roverId));
        }
    }
}
