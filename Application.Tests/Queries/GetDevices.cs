using Application.Commands;
using Application.Queries;
using Domain;
using FluentAssertions;
using TestData;

namespace Application.Tests;

[TestFixture]
public class GetDevicesTests : TestBase
{
    [Test]
    public async Task WhenDatabaseHasData_ShouldReturnDevices()
    {
        // Arrange
        var nrOfDevices = 5;
        var tasks = Enumerable.Range(0, nrOfDevices).Select(_ => Mediator.Send(DeviceData.CreateValidDeviceCommand()));
        await Task.WhenAll(tasks);

        // Act
        var query = new GetDevicesQuery();
        var result = await Mediator.Send(query);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().HaveCount(nrOfDevices)
            .And.OnlyHaveUniqueItems(device => device.Id);
    }

    [Test]
    public async Task WhenDatabaseIsEmpty_ShouldReturnEmptyList()
    {
        // Act
        var query = new GetDevicesQuery();
        var result = await Mediator.Send(query);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEmpty();
    }

    [Test]
    public async Task WhenFilteredByBrand_ShouldOnlyReturnMatchingDevices()
    {
        // Arrange
        var brandToInclude = "brandToInclude";
        var brandToExclude = "brandToExclude";
        await Mediator.Send(DeviceData.CreateValidDeviceCommand(brand: brandToInclude));
        await Mediator.Send(DeviceData.CreateValidDeviceCommand(brand: brandToExclude));

        // Act
        var query = new GetDevicesQuery(Brand: brandToInclude);
        var result = await Mediator.Send(query);

        // Assert
        result.Value.Should().AllSatisfy(device => device.Brand.Should().Be(brandToInclude));
    }

    [Test]
    public async Task Handle_WhenFilteredByState_ShouldReturnCorrectItems()
    {
        // Arrange
        var stateToInclude = DeviceState.Available;
        var stateToExclude = DeviceState.Inactive;
        await Mediator.Send(DeviceData.CreateValidDeviceCommand(state: stateToInclude));
        await Mediator.Send(DeviceData.CreateValidDeviceCommand(state: stateToExclude));

        // Act
        var query = new GetDevicesQuery(State: stateToInclude);
        var result = await Mediator.Send(query);

        // Assert
        result.Value.Should().AllSatisfy(d => d.State.Should().Be(stateToInclude));
    }
}