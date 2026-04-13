using Domain;
using ErrorOr;
using MediatR;

namespace Application.Queries;

public class GetAllDevicesHandler(IDeviceRepository repository) : IRequestHandler<GetAllDevicesQuery, ErrorOr<List<Device>>>
{
    public async Task<ErrorOr<List<Device>>> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync();
    }
}