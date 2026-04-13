using Domain;

namespace API.Contracts;

public record UpdateDeviceRequest(
    string? Name,
    string? Brand,
    DeviceState? State);