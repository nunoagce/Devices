using Domain;
using TestData.Utils;

namespace TestData;

public static class NameData
{
    public static readonly StringErrorBundle[] InvalidNames = 
    [
        new("", [DeviceErrors.NameRequired]),
        new(null!, [DeviceErrors.NameRequired])
    ];

    public static readonly string[] ValidNames =
    {
        "S",
        "Smart Sensor 2000",
        "Kitchen-Monitor_01"
    };
}
