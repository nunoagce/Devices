using Application.Commands;
using Domain;

namespace TestData;

public static class DeviceData
{
    public static readonly string ValidName = NameData.ValidNames.First();
    public static readonly string ValidBrand = BrandData.ValidBrands.First();
    public const DeviceState ValidState = DeviceState.Inactive;

    public static Device CreateValidDevice(
        string? name = null,
        string? brand = null,
        DeviceState? state = null)
    {
        return Device.Create(
            name ?? ValidName,
            brand ?? ValidBrand,
            state ?? ValidState
        ).Value;
    }

    public static CreateDeviceCommand CreateValidDeviceCommand(
        string? name = null,
        string? brand = null,
        DeviceState? state = null)
    {
        return new CreateDeviceCommand(
            name ?? ValidName,
            brand ?? ValidBrand,
            state
        );
    }
}
