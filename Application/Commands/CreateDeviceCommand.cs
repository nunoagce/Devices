using Domain;
using ErrorOr;
using MediatR;

namespace Application.Commands;

public record CreateDeviceCommand(
    string Name,
    string Brand,
    DeviceState? DeviceState) : IRequest<ErrorOr<Guid>>;
