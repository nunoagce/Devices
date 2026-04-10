using Domain.Tests.Utils;

namespace Domain.Tests.Data;

internal static class BrandData
{
    internal static readonly StringErrorBundle[] InvalidBrands =
    [
        new("", [DeviceErrors.BrandRequired]),
        new(null!, [DeviceErrors.BrandRequired]),
        new(new string('A', 15), [DeviceErrors.BrandTooLong]),
        new(new string(' ', 15), [DeviceErrors.BrandRequired, DeviceErrors.BrandTooLong]),
    ];

    internal static readonly string[] ValidBrands =
    {
        "Brand Name",
        "A",
        new string('A', 14)
    };
}
