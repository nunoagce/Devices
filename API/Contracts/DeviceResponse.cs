using Domain;

namespace API.Contracts;

public record DeviceResponse(
    Guid Id,
    string Name,
    string Brand,
    DeviceState State,
    DateTime CreationTime);
