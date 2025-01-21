# Mars Rover

This is a console app to simulate navigating a rover across the Martian surface. The user is first asked to specify the size of the plateau to explore, and then an initial position and facing direction on that surface. The user may then provide instructions: 

- M = move one grid unit
- L = rotate 90 degrees to the left
- R = rotate 90 degrees to the right
- Q = quit the program

## Run and test in Visual Studio (2022)

Load `MarsRover.sln` in Visual Studio, from where the project can be run and tested using Visual Studio's user interface.

## Running from the command line

1. Clone or download the repository.
2. `cd` into the repo folder:

```
cd mars-rover
```

3. `cd` into the `MarsRover` project.

```
cd MarsRover
```

4. Run the project:

```
dotnet run
```

This will build and run the project. 


## Run the tests from the commandline

1. `cd` into the tests folder in the repo:

```
cd MarsRover.Tests
```

2. Run the tests with:

```
dotnet test
```

## Implementation according to the functional programming paradigm

I've used a pure functional approach to structuring the app. This means _inter alia_ using:

- Value types instead of reference types. For example, the `MissionControl` class never changes state when the rover changes position. Instead, a new `MissionControl` class is created with a new rover each time there is a change.
- Recursion instead of loops.
- Monadic types from the excellent [LanguageExt](https://github.com/louthy/language-ext) library, especially `Option` and `Either`. The use of the latter means that it is never necessary to `throw` and `catch` exceptions. Instead, functions that may not succeed return an `Either<string, T>`, where `T` is the return type of the corresponding non-monadic function. If the function succeeds, `T` is returned wrapped in the `Either` monad. If it fails, a `string` carrying the error message is returned, also wrapped in the `Either`. (For an introduction to the practice and advantages of functional programming in C#, see [Paul's Louth's blog](https://paullouth.com/). On monads in particular, see [his introduction](https://paullouth.com/higher-kinds-in-csharp-with-language-ext-part-7-monads/).)

One of the great benefits of wrapping return values in monadic types, like `Either`, is that it is possible to use LINQ query expressions with them. Consider the following code from the `ConsoleUI` of the `MarsRover` project:

```C#
public static Either<string, MissionControl> GetInitialSetup()
{
    return  from plateauSize in GetPlateauSize(None)
            from plateau in Plateau.FromPlateauSize(plateauSize)
            from missionControl in MissionControl.FromPlateau(plateau)
            from position in GetInitialPosition(plateauSize, None)
            from updatedMissionControl in missionControl.AddRover(position)
            select updatedMissionControl;
}
```

As [Paul Louth shows](https://paullouth.com/higher-kinds-in-csharp-with-language-ext-part-7-monads/), this structure parallels `do` notation in Haskell. The equivalent function in Haskell might be written like this:

```Haskell
GetInitialSetup :: Either String MissionControl
GetInitialSetup = do
    plateauSize <- GetPlateauSize Nothing
    plateau <- Plateau.FromPlateauSize plateauSize
    missionControl <- MissionControl.FromPlateau plateau
    position <- GetInitialPosition PlateauSize Nothing
    updatedMissionControl <- AddRover missionControl position
    pure updatedMissionControl

```



## Acknowledgements

### Context

This project was written as a learning exercise as part of the [Northcoders](https://northcoders.com/) bootcamp.

### Dependencies and licenses
The main project, `MarsRover` has one dependency:
- [LanguageExt](https://github.com/louthy/language-ext): Extensions for functional programming in C# ([MIT](https://github.com/louthy/language-ext?tab=MIT-1-ov-file#readme))

The test project depends on:
- [NUnit](https://github.com/nunit/nunit) ([MIT](https://github.com/nunit/nunit?tab=MIT-1-ov-file#readme))
- [Coverlet](https://github.com/coverlet-coverage/) ([MIT](https://github.com/coverlet-coverage/coverlet?tab=License-1-ov-file))