using Domain;
using System.ComponentModel.DataAnnotations;

namespace API.Contracts;

public record CreateDeviceRequest(
    [Required] string Name,
    [Required] string Brand,
    [EnumDataType(typeof(DeviceState))] DeviceState? State);
