using Application.Commands;
using Application.Queries;
using Domain;
using ErrorOr;
using FluentAssertions;
using TestData;

namespace Application.Tests.Commands;

[TestFixture]
public class DeleteDeviceTests : TestBase
{
    [Test]
    public async Task WhenValid_ShouldRemoveDeviceFromDatabase()
    {
        // Arrange
        var createResult = await Mediator.Send(DeviceData.CreateValidDeviceCommand());
        var deviceId = createResult.Value;

        // Act
        var deleteResult = await Mediator.Send(new DeleteDeviceCommand(deviceId));

        // Assert
        deleteResult.IsError.Should().BeFalse();
        deleteResult.Value.Should().Be(Result.Deleted);

        // Verify it's actually gone
        var getResult = await Mediator.Send(new GetDeviceByIdQuery(deviceId));
        getResult.IsError.Should().BeTrue();
        getResult.FirstError.Type.Should().Be(ErrorType.NotFound);
    }

    [Test]
    public async Task WhenDeviceIsInUse_ShouldReturnError()
    {
        // Arrange
        var createResult = await Mediator.Send(DeviceData.CreateValidDeviceCommand());
        var deviceId = createResult.Value;

        await Mediator.Send(new UpdateDeviceCommand(deviceId, null, null, DeviceState.InUse));

        // Act
        var deleteResult = await Mediator.Send(new DeleteDeviceCommand(deviceId));

        // Assert
        deleteResult.IsError.Should().BeTrue();
        deleteResult.FirstError.Should().Be(DeviceErrors.CannotDeleteInUse);

        // Verify the device still exists in the DB
        var getResult = await Mediator.Send(new GetDeviceByIdQuery(deviceId));
        getResult.IsError.Should().BeFalse();
    }
}
