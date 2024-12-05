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
}
