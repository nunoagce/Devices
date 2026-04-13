using Domain;
using ErrorOr;
using MediatR;

namespace Application.Queries;

public class GetDeviceByIdHandler(IDeviceRepository repository) : IRequestHandler<GetDeviceByIdQuery, ErrorOr<Device>>
{
    public async Task<ErrorOr<Device>> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await repository.GetByIdAsync(request.DeviceId);

        return device is null
            ? DeviceErrors.NotFoundId(request.DeviceId)
            : device;
    }
}