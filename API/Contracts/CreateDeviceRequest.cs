using Domain;
using System.ComponentModel.DataAnnotations;

namespace API.Contracts;

public record CreateDeviceRequest(
    [Required] string Name,
    [Required] string Brand,
    [EnumDataType(typeof(State))] State? State);
