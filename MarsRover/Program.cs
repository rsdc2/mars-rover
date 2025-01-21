using MarsRover.UI;

var mc__ = from mc in ConsoleUI.GetInitialSetup()
            from mc_ in ConsoleUI.HandleUserInstructions(mc, mc.Description())
            select mc_;
