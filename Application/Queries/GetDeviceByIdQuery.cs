using Domain;
using ErrorOr;
using MediatR;

namespace Application.Queries;

public record GetDeviceByIdQuery(Guid DeviceId) : IRequest<ErrorOr<Device>>;
