using API.Contracts;
using API.Utils;
using Application.Commands;
using Application.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
public class DevicesController : ApiController
{
    private readonly ISender _sender;

    public DevicesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDevices()
    {
        var query = new GetAllDevicesQuery();

        var result = await _sender.Send(query);

        return result.Match(
            devices => Ok(devices.ConvertAll(ToDto)),
            Problem);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDevice(CreateDeviceRequest request)
    {
        var command = new CreateDeviceCommand(request.Name, request.Brand, request.State);

        var result = await _sender.Send(command);

        return result.Match(
            deviceId => CreatedAtAction(nameof(GetById), new { deviceId }, deviceId),
            Problem);
    }

    [HttpGet("{deviceId:guid}")]
    public async Task<IActionResult> GetById(Guid deviceId)
    {
        var query = new GetDeviceByIdQuery(deviceId);

        var result = await _sender.Send(query);

        return result.Match(
            device => Ok(ToDto(device)),
            Problem);
    }

    private DeviceResponse ToDto(Device device) =>
        new(device.Id, device.Name, device.Brand, device.State, device.CreationTime);
}