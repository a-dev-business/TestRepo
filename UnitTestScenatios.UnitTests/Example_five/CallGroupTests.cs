using FluentAssertions;
using UnitTestScenatios.Example_five;

namespace UnitTestScenatios.UnitTests.Example_five;

public class CallGroupTests
{
    [Fact]
    public void Constructor_Initialization_ShouldThrow_ArgumentNullException_WhenParticipantsCountIsLessThanOrEqualToZero()
    {
        Func<IReadOnlyCollection<string>, Task> func = _ => Task.FromResult(0); 
        var zeroConstructionAction = () => new CallGroup<string>(0, func, TimeSpan.FromSeconds(1));
        var lessThanZeroConstructionAction = () => new CallGroup<string>(-1, func, TimeSpan.FromSeconds(1));

        zeroConstructionAction.Should().Throw<ArgumentException>().WithMessage("Value 0 should be greater than zero! (Parameter 'participantCount')");
        lessThanZeroConstructionAction.Should().Throw<ArgumentException>().WithMessage("Value -1 should be greater than zero! (Parameter 'participantCount')");
    }
    
    [Fact]
    public async Task GroupCall_Should_Invoke_When_All_Participants_Join()
    {
        // Arrange
        var operations = new List<string>();
        var groupCalled = false;

        var callGroup = new CallGroup<string>(
            participantCount: 3,
            groupCallDelegate: async (ops) =>
            {
                groupCalled = true;
                operations.AddRange(ops);
                await Task.CompletedTask;
            },
            timeout: TimeSpan.FromSeconds(5)
        );

        // Act
        var t1 = callGroup.Join("User1");
        var t2 = callGroup.Join("User2");
        var t3 = callGroup.Join("User3");

        await Task.WhenAll(t1, t2, t3);

        // Assert
        groupCalled.Should().BeTrue();
        operations.Count.Should().Be(3);
        operations.Should().Contain("User1");
        operations.Should().Contain("User2");
        operations.Should().Contain("User3");
    }
    
    [Fact]
    public async Task GroupCall_Should_Throw_When_Too_Many_Join()
    {
        // Arrange
        var callGroup = new CallGroup<string>(
            participantCount: 2,
            groupCallDelegate: async (ops) => await Task.CompletedTask,
            timeout: TimeSpan.FromSeconds(5)
        );

        var joinATask = callGroup.Join("A");
        var joinBTask = callGroup.Join("B");
        
        await Task.WhenAll(joinATask, joinBTask);

        // Act & Assert
        var joinExtraAction = () => callGroup.Join("Extra");
        await joinExtraAction.Should().ThrowAsync<Exception>().WithMessage("Too many participants!");
    }
    
    
    [Fact]
    public async Task GroupCall_Should_Complete_Using_Leave()
    {
        // Arrange
        var groupCalled = false;

        var callGroup = new CallGroup<string>(
            participantCount: 2,
            groupCallDelegate: async (ops) =>
            {
                groupCalled = true;
                await Task.CompletedTask;
            },
            timeout: TimeSpan.FromSeconds(2)
        );

        // Act
        var t1 = callGroup.Join("WithData");
        callGroup.Leave();

        await t1;

        // Assert
        Assert.True(groupCalled);
    }
    
}