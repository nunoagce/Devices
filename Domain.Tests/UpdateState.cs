using Domain.Tests.Data;
using FluentAssertions;

namespace Domain.Tests;

[TestFixture]
public class TransitionToState
{
    [Test]
    public void WhenNewStateIsProvided_ShouldUpdateState()
    {
        // Arrange
        var initialState = DeviceState.Available;
        var newState = DeviceState.InUse;
        var device = DeviceData.CreateValidDevice(state: initialState);

        // Act
        var result = device.UpdateState(newState);

        // Assert
        result.IsError.Should().BeFalse();
        device.State.Should().Be(newState);
    }
}