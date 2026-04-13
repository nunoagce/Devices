using Application.Commands;
using Application.Queries;
using Domain;
using ErrorOr;
using FluentAssertions;
using TestData;

namespace Application.Tests.Commands;

[TestFixture]
public class UpdateDevice : TestBase
{
    [Test]
    public async Task WhenFullUpdate_ShouldUpdateAllFields()
    {
        // Arrange
        var createCommand = DeviceData.CreateValidDeviceCommand();
        var createResult = await Mediator.Send(createCommand);
        var deviceId = createResult.Value;

        var updateCommand = new UpdateDeviceCommand(
            Id: deviceId,
            Name: "Updated Name",
            Brand: "Updated Brand",
            State: DeviceState.InUse);

        // Act
        var updateResult = await Mediator.Send(updateCommand);

        // Assert
        updateResult.IsError.Should().BeFalse();
        updateResult.Value.Should().Be(Result.Success);

        // Act
        var getResult = await Mediator.Send(new GetDeviceByIdQuery(deviceId));

        // Assert
        getResult.Value.Name.Should().Be(updateCommand.Name).And.NotBe(createCommand.Name);
        getResult.Value.Brand.Should().Be(updateCommand.Brand).And.NotBe(createCommand.Brand);
        getResult.Value.State.Should().Be(updateCommand.State).And.NotBe(createCommand.State);
    }

    [Test]
    public async Task WhenPartialUpdate_ShouldOnlyUpdateProvidedFields()
    {
        // Arrange
        var createCommand = DeviceData.CreateValidDeviceCommand();
        var createResult = await Mediator.Send(createCommand);
        var deviceId = createResult.Value;

        // Act
        var updateCommand = new UpdateDeviceCommand(
            Id: deviceId,
            Name: "Just The Name Changed");

        var updateResult = await Mediator.Send(updateCommand);

        // Assert
        updateResult.IsError.Should().BeFalse();

        // Act
        var getResult = await Mediator.Send(new GetDeviceByIdQuery(deviceId));

        // Assert
        getResult.Value.Name.Should().Be(updateCommand.Name).And.NotBe(createCommand.Name);
        getResult.Value.Brand.Should().Be(createCommand.Brand).And.NotBe(updateCommand.Brand);
        getResult.Value.State.Should().NotBe(updateCommand.State);
    }
}