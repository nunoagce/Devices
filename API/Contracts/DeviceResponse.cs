using Domain;

namespace API.Contracts;

public record DeviceResponse(
    Guid Id,
    string Name,
    string Brand,
    State State,
    DateTime CreationTime);
