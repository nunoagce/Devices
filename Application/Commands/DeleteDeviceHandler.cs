
using Domain;
using ErrorOr;
using MediatR;

namespace Application.Commands;

public class DeleteDeviceHandler(IDeviceRepository repository)
    : IRequestHandler<DeleteDeviceCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteDeviceCommand command, CancellationToken cancellationToken)
    {
        var device = await repository.GetByIdAsync(command.Id);

        if (device is null) return DeviceErrors.NotFoundId(command.Id);

        var deleteResult = device.Delete();

        if (deleteResult.IsError) return deleteResult.Errors;

        await repository.RemoveAsync(device);

        return Result.Deleted;
    }
}