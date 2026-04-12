using Application;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DevicesController : ControllerBase
{
    private readonly IDeviceRepository _repository;

    public DevicesController(IDeviceRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetDevices()
    {
        var devices = await _repository.GetAllAsync();
        return Ok(devices);
    }
}