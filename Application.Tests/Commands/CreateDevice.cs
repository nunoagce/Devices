using Application.Commands;
using Application.Queries;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestData;

namespace Application.Tests.Commands;

[TestFixture]
public class CreateDeviceTests : TestBase
{
    [Test]
    public async Task WhenSuccessful_ShouldReturnTheCreatedDevice()
    {
        // Arrange
        var createCommand = DeviceData.CreateValidDeviceCommand();

        // Act
        var createResult = await Mediator.Send(createCommand);

        // Assert
        createResult.IsError.Should().BeFalse();

        // Act
        var newId = createResult.Value;
        var getQuery = new GetDeviceByIdQuery(newId);
        var getResult = await Mediator.Send(getQuery);

        // Assert
        getResult.IsError.Should().BeFalse();
        getResult.Value.Should().NotBeNull();
        getResult.Value.Id.Should().Be(newId);
        getResult.Value.Name.Should().Be(createCommand.Name);
        getResult.Value.Brand.Should().Be(createCommand.Brand);
    }

    [Test]
    public async Task CreateThenGetAll_WhenCreateFails_ShouldReturnEmptyList()
    {
        // Arrange
        var invalidCommand = DeviceData.CreateValidDeviceCommand(name: "");

        // Act
        var createResult = await Mediator.Send(invalidCommand);

        // Assert
        createResult.IsError.Should().BeTrue("because validation should prevent creation");

        // Act
        var getAllQuery = new GetAllDevicesQuery();
        var getAllResult = await Mediator.Send(getAllQuery);

        // Assert
        getAllResult.IsError.Should().BeFalse();
        getAllResult.Value.Should().BeEmpty("because the previous creation failed");
    }
}