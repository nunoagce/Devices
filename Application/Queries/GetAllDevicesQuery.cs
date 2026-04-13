using MediatR;
using Domain;
using ErrorOr;

namespace Application.Queries;

public record GetAllDevicesQuery() : IRequest<ErrorOr<List<Device>>>;