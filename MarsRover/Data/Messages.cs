using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Data;

internal static class Messages
{
    public static Dictionary<Failures, string> MessagesDict = new()
    {
        {Failures.CommandsNotCarriedOut, CommandsNotCarriedOut},
        {Failures.ParseFailure, ParseFailure}
    };

    public static string CommandsNotCarriedOut = "Commands not carried out";
    public static string ParseFailure = "could not be parsed";
    public static string NoInstruction = "No instruction given";
    public static string NoPosition = "No position given";


    public static string InvalidDirection(char direction)
    {
        return $"Direction {direction} is not valid";
    }

    public static string InvalidCoordinate(char coordinate)
    {
        return $"{coordinate} is not a valid coordinate";
    }

    public static string InvalidPosition(string position)
    {
        return $"Position {position} is not valid";
    }


}
