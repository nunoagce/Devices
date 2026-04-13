using TestData;
using TestData.Utils;
using FluentAssertions;

namespace Domain.Tests
{
    [TestFixture]
    public class Create
    {
        [Test]
        public void WhenValidArguments_ShouldReturnDevice()
        {
            // Arrange
            var name = DeviceData.ValidName;
            var brand = DeviceData.ValidBrand;
            var state = DeviceData.ValidState;

            // Act
            var result = Device.Create(name, brand, state);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Id.Should().NotBeEmpty();
            result.Value.Name.Should().Be(name);
            result.Value.Brand.Should().Be(brand);
            result.Value.State.Should().Be(state);
            result.Value.CreationTime.Should().NotBe(default);
        }


        [TestCaseSource(typeof(NameData), nameof(NameData.ValidNames))]
        public void WhenNameIsValid_ShouldSucceed(string validName)
        {
            // Act
            var result = Device.Create(validName, DeviceData.ValidBrand);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Name.Should().Be(validName);
        }

        [TestCaseSource(typeof(NameData), nameof(NameData.InvalidNames))]
        public void WhenNameIsInvalid_ShouldReturnNameError(StringErrorBundle nameBundle)
        {
            // Act
            var result = Device.Create(nameBundle.Value, DeviceData.ValidBrand, DeviceData.ValidState);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().BeEquivalentTo(nameBundle.ExpectedErrors);
        }

        [TestCaseSource(typeof(BrandData), nameof(BrandData.ValidBrands))]
        public void WhenBrandIsValid_ShouldSucceed(string validBrand)
        {
            // Act
            var result = Device.Create(DeviceData.ValidName, validBrand, DeviceData.ValidState);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Brand.Should().Be(validBrand);
        }

        [TestCaseSource(typeof(BrandData), nameof(BrandData.InvalidBrands))]
        public void WhenBrandIsInvalid_ShouldReturnBrandError(StringErrorBundle brandBundle)
        {
            // Act
            var result = Device.Create(DeviceData.ValidName, brandBundle.Value, DeviceData.ValidState);

            //Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().BeEquivalentTo(brandBundle.ExpectedErrors);
        }

        [Test, Combinatorial]
        public void WhenNameAndBrandIsInvalid_ShouldReturnAllExpectedErrors(
            [ValueSource(typeof(NameData), nameof(NameData.InvalidNames))] StringErrorBundle nameBundle,
            [ValueSource(typeof(BrandData), nameof(BrandData.InvalidBrands))] StringErrorBundle brandBundle)
        {
            // Arrange
            var totalExpectedErrors = nameBundle.ExpectedErrors
                .Concat(brandBundle.ExpectedErrors)
                .ToList();

            // Act
            var result = Device.Create(nameBundle.Value, brandBundle.Value, DeviceData.ValidState);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().BeEquivalentTo(totalExpectedErrors);
        }

        [Test]
        public void WhenStateNotProvided_ShouldDefaultToInactive()
        {
            // Act
            var result = Device.Create(DeviceData.ValidName, DeviceData.ValidBrand);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.State.Should().Be(DeviceState.Inactive);
        }
    }
}