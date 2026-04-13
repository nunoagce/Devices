using Domain;
using TestData.Utils;

namespace TestData;

public static class BrandData
{
    public static readonly StringErrorBundle[] InvalidBrands =
    [
        new("", [DeviceErrors.BrandRequired]),
        new(null!, [DeviceErrors.BrandRequired]),
        new(new string('A', 15), [DeviceErrors.BrandTooLong]),
        new(new string(' ', 15), [DeviceErrors.BrandRequired, DeviceErrors.BrandTooLong]),
    ];

    public static readonly string[] ValidBrands =
    {
        "Brand Name",
        "A",
        new string('A', 14)
    };
}
