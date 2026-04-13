using Domain;
using ErrorOr;
using MediatR;

namespace Application.Queries;

public class GetDevicesHandler(IDeviceRepository repository) : IRequestHandler<GetDevicesQuery, ErrorOr<List<Device>>>
{
    public async Task<ErrorOr<List<Device>>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
    {
        return await repository.ListAsync(request.Brand, request.State);
    }
}