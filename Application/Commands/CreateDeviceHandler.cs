using Domain;
using ErrorOr;
using MediatR;

namespace Application.Commands;

public class CreateDeviceHandler(IDeviceRepository repository) : IRequestHandler<CreateDeviceCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        var createDeviceResult = Device.Create(request.Name, request.Brand, request.State);

        if (createDeviceResult.IsError) return createDeviceResult.Errors;

        await repository.AddAsync(createDeviceResult.Value);

        return createDeviceResult.Value.Id;
    }
}
