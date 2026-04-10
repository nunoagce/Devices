namespace Domain.Tests.Data;

internal static class DeviceData
{
    internal static readonly string ValidName = NameData.ValidNames.First();
    internal static readonly string ValidBrand = BrandData.ValidBrands.First();
    internal const DeviceState ValidState = DeviceState.Inactive;

    internal static Device CreateValidDevice(
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
}
