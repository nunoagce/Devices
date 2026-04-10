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
}
