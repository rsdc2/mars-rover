using FluentAssertions;
using MarsRover.Types;

namespace MarsRover.Tests.Types;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test, Description("Test that creates a Success Either from static method without message")]
    public void CreateSuccessFromStaticMethodNoMessage__FromOverload()
    {
        // Arrange
        int result = 3;

        // Act
        var either = Either<int>.From(result);

        // Assert
        Assert.That(either.Value is Success<int>);
    }

    [Test, Description("Test that creates a Success Either from static method with message")]
    public void CreateSuccessFromStaticMethodWithMessage()
    {
        // Arrange
        int result = 3;

        // Act
        var either = Either<int>.FromSuccess(result, "Operation successful");

        // Assert
        Assert.That(either.Value is Success<int>);
    }

    [Test, Description("Test that creates a Failure Either from static method")]
    public void CreateFailureFromStaticMethod()
    {
        // Arrange

        // Act
        var either = Either<int>.FromFailure("Operation successful");

        // Assert
        Assert.That(either.Value is Failure);
    }

    [Test, Description("Test that creates a Failure Either from static method using From overload")]
    public void CreateFailureFromStaticMethod__FromOverload()
    {
        // Arrange

        // Act
        var either = Either<int>.From("Operation successful");

        // Assert
        Assert.That(either.Value is Failure);
    }

    [Test, Description("Test that unwrap a list of eithers into an either of List")]
    public void UnwrapAListOfEithersIntoAnEitherOfList()
    {
        // Arrange
        var either1 = Either<int>.From(1);
        var either2 = Either<int>.From(2);

        // Act
        var unwrapped = Either<int>.Unwrap([either1, either2]);

        // Assert
        Assert.That(unwrapped.Value is Success<List<int>>);
    }


    [Test]
    public void GetFailuresTest()
    {
        // Arrange
        var either = Either<int>.From("Could not perform calculation");
        List <Either<int>> eithers = [either];

        // Act
        var failures = Either<int>.Failures(eithers);

        // Assert
        failures[0].Message.Should().Be("Could not perform calculation");
    }

    [Test]
    public void UnwrapAListOfASingleFailureIntoASingleFailure()
    {
        // Arrange
        var either = Either<int>.From("Could not perform calculation");


        // Act
        var unwrapped = Either<int>.Unwrap([either]);

        // Assert
        Assert.That(unwrapped.Value is Failure);
        unwrapped.Value.Message.Should().Be("Could not perform calculation");
    }

    [Test]
    public void UnwrapAListOfEithersWithOneFailureIntoAFailure()
    {
        // Arrange
        var either1 = Either<int>.From("Could not perform calculation");
        var either2 = Either<int>.From(2);

        // Act
        var unwrapped = Either<int>.Unwrap([either1, either2]);

        // Assert
        Assert.That(unwrapped.Value is Failure);
        Assert.That(unwrapped.Value.Message == "Could not perform calculation");
    }

    [Test, Description("Test that unwrap a list of eithers into an either of List")]
    public void UnwrapAListOfEitherWithMultipleFailuresIntoAFailure()
    {
        // Arrange
        var either1 = Either<int>.From("Could not perform calculation");
        var either2 = Either<int>.From("Could not perform calculation");

        // Act
        var unwrapped = Either<int>.Unwrap([either1, either2]);

        // Assert
        Assert.That(unwrapped.Value is Failure);
        unwrapped.Value.Message.Should().Be("Could not perform calculation\nCould not perform calculation");
    }

    [Test]
    public void FmapSuccessTest()
    {
        // Arrange
        var either = Either<int>.From(1);
        Func<int, int> add1 = x => x + 1;

        // Act
        var result = (Success<int>)either.Fmap(add1).Value;

        // Assert
        result.Result.Should().Be(2);
    }

    [Test]
    public void FmapFailureTest()
    {
        // Arrange
        var either = Either<int>.From("Could not process instruction");
        Func<int, int> add1 = x => x + 1;

        // Act
        var result = either.Fmap(add1);

        // Assert
        Assert.That(result.Value is Failure);
    }


}