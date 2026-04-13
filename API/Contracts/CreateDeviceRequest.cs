using Domain;

namespace API.Contracts;

public record CreateDeviceRequest(
    string Name,
    string Brand,
    DeviceState? DeviceState);
