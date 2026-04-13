using FluentAssertions;
using TestData;
using TestData.Utils;

namespace Domain.Tests;

[TestFixture]
public class UpdateName
{
    [TestCaseSource(typeof(NameData), nameof(NameData.ValidNames))]
    public void WhenNameIsValid_ShouldReturnSuccess(string validName)
    {
        // Arrange
        var device = DeviceData.CreateValidDevice();

        // Act
        var result = device.UpdateName(validName);

        // Assert
        result.IsError.Should().BeFalse();
        device.Name.Should().Be(validName);
    }

    [TestCaseSource(typeof(NameData), nameof(NameData.InvalidNames))]
    public void WhenNameIsInvalid_ShouldReturnError(StringErrorBundle nameBundle)
    {
        // Arrange
        var device = DeviceData.CreateValidDevice();
        var initialName = device.Name;

        // Act
        var result = device.UpdateName(nameBundle.Value);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().BeEquivalentTo(nameBundle.ExpectedErrors);
        device.Name.Should().Be(initialName);
    }

    [Test]
    public void WhenDeviceIsInUse_ShouldReturnError()
    {
        // Arrange
        var device = DeviceData.CreateValidDevice(state: State.InUse);
        var initialName = device.Name;

        // Act
        var result = device.UpdateName("NewName");

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Should().Be(DeviceErrors.CannotUpdateNameInUse);
        device.Name.Should().Be(initialName);
    }
}