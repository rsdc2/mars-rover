using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Model;

namespace MarsRover.Data;


internal static class Messages
{
    public static string GetPlateauSize = "Please enter plateau dimensions, e.g. '5 5':";
    public static string GetInitialPosition = "Please enter an initial position and a facing direction, e.g. '5 5 N':";
    public static string GetInstructions = "Please enter an instruction:";


    public static string CannotAddPlateau = "Cannot add plateau";
    public static string CannotAddRover = "Cannot add Rover";
    public static string CommandsNotCarriedOut = "Commands not carried out";
    public static string EmptyInput = "No input received";
    public static string ParseFailure = "could not be parsed";
    public static string NoInstruction = "No instruction given";
    public static string NoPosition = "No position given";
    public static string Unforeseen = "Unforeseen";
    public static string QuitMessage = "Quitting...";

    public static string CannotGetPositionDataFromString(string input, string message)
    {
        return $"Cannot get position data from string '{input}': {message}";
    }
    public static string CannotMoveRover(int roverId)
    {
        return $"Cannot move Rover {roverId}";
    }

    public static string CannotParseStringToInteger(string str, string message)
    {
        return $"Cannot parse string '{str}' to integer: {message}";
    }

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
    public static string InvalidDirection(string direction)
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

    public static string MoreThanOneRoverWithId(int id)
    {
        return $"More than one rover with {id} exists";
    }

    public static string RoverDoesNotExist(int id)
    {
        return $"Rover with id {id} does not exist";
    }


}
