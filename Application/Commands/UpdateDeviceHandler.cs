using Domain;
using ErrorOr;
using MediatR;

namespace Application.Commands;

public class UpdateDeviceHandler(IDeviceRepository repository)
    : IRequestHandler<UpdateDeviceCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UpdateDeviceCommand command, CancellationToken cancellationToken)
    {
        var device = await repository.GetByIdAsync(command.Id);
        if (device is null) return DeviceErrors.NotFoundId(command.Id);

        List<Error> errors = [];

        if (command.Name is not null)
            device.UpdateName(command.Name).Switch(success => { }, errors.AddRange);

        if (command.Brand is not null)
            device.UpdateBrand(command.Brand).Switch(success => { }, errors.AddRange);

        if (command.State.HasValue)
            device.UpdateState(command.State.Value).Switch(success => { }, errors.AddRange);

        if (errors.Count > 0) return errors;

        await repository.UpdateAsync(device);

        return Result.Success;
    }
}