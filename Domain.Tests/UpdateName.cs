using Domain.Tests.Data;
using Domain.Tests.Utils;
using FluentAssertions;

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
}