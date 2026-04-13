using Domain;
using ErrorOr;
using MediatR;

namespace Application.Commands;

public record UpdateDeviceCommand(
    Guid Id,
    string? Name = null,
    string? Brand = null,
    State? State = null) : IRequest<ErrorOr<Success>>;
