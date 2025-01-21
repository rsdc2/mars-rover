namespace MarsRover;
using MarsRover.UI;

internal class Program
{
    static void Main(string[] args)
    {
        var mc__ = from mc in ConsoleUI.GetInitialSetup()
                   from mc_ in ConsoleUI.HandleUserInstructions(mc, mc.Description())
                   select mc_;
    }
}
