using ErrorOr;
using MediatR;

namespace Application.Commands;

public record DeleteDeviceCommand(Guid Id) : IRequest<ErrorOr<Deleted>>;