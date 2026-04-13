using Application.Queries;
using FluentAssertions;
using TestData;

namespace Application.Tests;

[TestFixture]
public class GetAllDevicesTests : TestBase
{
    [Test]
    public async Task WhenDatabaseHasData_ShouldReturnDevices()
    {
        // Arrange
        var nrOfDevices = 5;
        var tasks = Enumerable.Range(0, nrOfDevices).Select(_ => Mediator.Send(DeviceData.CreateValidDeviceCommand()));
        await Task.WhenAll(tasks);

        // Act
        var query = new GetAllDevicesQuery();
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
        var query = new GetAllDevicesQuery();
        var result = await Mediator.Send(query);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEmpty();
    }
}