using MediatR;
using Domain;
using ErrorOr;

namespace Application.Queries;

public record GetDevicesQuery(
    string? Brand = null,
    DeviceState? State = null) : IRequest<ErrorOr<List<Device>>>;