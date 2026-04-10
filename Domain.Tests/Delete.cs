using Domain.Tests.Data;
using ErrorOr;
using FluentAssertions;

namespace Domain.Tests;

[TestFixture]
public class Delete
{
    [Test]
    public void WhenStateIsNotInUse_ShouldReturnDeleted()
    {
        // Arrange
        var device = DeviceData.CreateValidDevice(state: DeviceState.Available);

        // Act
        var result = device.Delete();

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Result.Deleted);
    }

    [Test]
    public void WhenStateIsInUse_ShouldReturnError()
    {
        // Arrange
        var device = DeviceData.CreateValidDevice(state: DeviceState.InUse);

        // Act
        var result = device.Delete();

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DeviceErrors.CannotDeleteInUse);
    }
}