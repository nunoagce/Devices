using FluentAssertions;
using TestData;
using TestData.Utils;

namespace Domain.Tests;

[TestFixture]
public class UpdateBrand
{
    [TestCaseSource(typeof(BrandData), nameof(BrandData.ValidBrands))]
    public void WhenBrandIsValid_ShouldUpdateProperty(string validBrand)
    {
        // Arrange
        var device = DeviceData.CreateValidDevice();

        // Act
        var result = device.UpdateBrand(validBrand);

        // Assert
        result.IsError.Should().BeFalse();
        device.Brand.Should().Be(validBrand);
    }

    [TestCaseSource(typeof(BrandData), nameof(BrandData.InvalidBrands))]
    public void WhenBrandIsInvalid_ShouldReturnError(StringErrorBundle brandBundle)
    {
        // Arrange
        var device = DeviceData.CreateValidDevice();
        var originalBrand = device.Brand;

        // Act
        var result = device.UpdateBrand(brandBundle.Value);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().BeEquivalentTo(brandBundle.ExpectedErrors);
        device.Brand.Should().Be(originalBrand);
    }

    [Test]
    public void WhenDeviceIsInUse_ShouldReturnError()
    {
        // Arrange
        var device = DeviceData.CreateValidDevice(state: DeviceState.InUse);
        var initialBrand = device.Brand;

        // Act
        var result = device.UpdateBrand("NewBrand");

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Should().Be(DeviceErrors.CannotUpdateBrandInUse);
        device.Brand.Should().Be(initialBrand);
    }
}