using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Model;

namespace MarsRover.Data;


internal static class Messages
{
    public static string CannotAddPlateau = "Cannot add plateau";
    public static string CannotAddRover = "Cannot add Rover";
    public static string CommandsNotCarriedOut = "Commands not carried out";
    public static string EmptyInput = "No input received";
    public static string ParseFailure = "could not be parsed";
    public static string NoInstruction = "No instruction given";
    public static string NoPosition = "No position given";

    public static string CannotRotateRover(Rover rover)
    {
        return $"Cannot rotate Rover {rover.Id}";
    }

    public static string CannotRotateRover(int roverId)
    {
        return $"Cannot rotate Rover {roverId}";
    }

    public static string InvalidDirection(char direction)
    {
        return $"Direction {direction} is not valid";
    }

    public static string InvalidCoordinate(char coordinate)
    {
        return $"{coordinate} is not a valid coordinate";
    }

    public static string InvalidDimensions(string dimensions)
    {
        return $"{dimensions} are not valid dimensions for the plateau";
    }

    public static string InvalidPosition(string position)
    {
        return $"Position {position} is not valid";
    }


}
