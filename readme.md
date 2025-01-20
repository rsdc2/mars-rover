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

## Acknowledgements

### Context

This project was written as a learning exercise as part of the [Northcoders](https://northcoders.com/) bootcamp.

### Dependencies and licenses
The main project, `MarsRover` has one dependency:
- [LanguageExt](https://github.com/louthy/language-ext): Extensions for functional programming in C# ([MIT](https://github.com/louthy/language-ext?tab=MIT-1-ov-file#readme))

The test project depends on:
- [NUnit](https://github.com/nunit/nunit) ([MIT](https://github.com/nunit/nunit?tab=MIT-1-ov-file#readme))
- [Coverlet](https://github.com/coverlet-coverage/) ([MIT](https://github.com/coverlet-coverage/coverlet?tab=License-1-ov-file))