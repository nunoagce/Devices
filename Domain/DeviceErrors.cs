using ErrorOr;

namespace Domain;

public static class DeviceErrors
{
    public static Error NameRequired => Error.Validation(
        code: "Device.NameRequired",
        description: "Name is required.");

    public static Error BrandRequired => Error.Validation(
        code: "Device.BrandRequired",
        description: "Brand is required.");

    public static Error BrandTooLong => Error.Validation(
        code: "Device.BrandTooLong",
        description: "Brand must be shorter than 15 characters.");

    public static Error CannotUpdateNameInUse => Error.Validation(
        code: "Device.CannotUpdateNameInUse",
        description: "Cannot update name while device is in use.");

    public static Error CannotUpdateBrandInUse => Error.Validation(
        code: "Device.CannotUpdateBrandInUse",
        description: "Cannot update brand while device is in use.");

    public static Error CannotDeleteInUse => Error.Validation(
        code: "Device.CannotDeleteInUse",
        description: "Cannot delete device while it is in use.");

    public static Error NotFoundId(Guid id) => Error.NotFound(
        code: "Device.NotFound",
        description: $"Device with id {id} was not found.");
}
