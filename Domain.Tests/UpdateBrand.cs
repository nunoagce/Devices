using Domain.Tests.Data;
using Domain.Tests.Utils;
using FluentAssertions;

namespace Domain.Tests;

[TestFixture]
public class UpdateBrand
{
    [TestCaseSource(typeof(BrandData), nameof(BrandData.ValidBrands))]
    public void WhenBrandIsValid_ShouldUpdateProperty(string validBrand)
    {
        var device = DeviceData.CreateValidDevice();

        var result = device.UpdateBrand(validBrand);

        result.IsError.Should().BeFalse();
        device.Brand.Should().Be(validBrand);
    }

    [TestCaseSource(typeof(BrandData), nameof(BrandData.InvalidBrands))]
    public void WhenBrandIsInvalid_ShouldReturnError(StringErrorBundle brandBundle)
    {
        var device = DeviceData.CreateValidDevice();
        var originalBrand = device.Brand;

        var result = device.UpdateBrand(brandBundle.Value);

        result.IsError.Should().BeTrue();
        result.Errors.Should().BeEquivalentTo(brandBundle.ExpectedErrors);
        device.Brand.Should().Be(originalBrand);
    }
}