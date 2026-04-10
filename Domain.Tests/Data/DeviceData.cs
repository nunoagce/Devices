namespace Domain.Tests.Data;

internal static class DeviceData
{
    internal static readonly string ValidName = NameData.ValidNames.First();
    internal static readonly string ValidBrand = BrandData.ValidBrands.First();
    internal const DeviceState ValidState = DeviceState.InUse;
}
