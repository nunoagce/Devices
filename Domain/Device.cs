using ErrorOr;

namespace Domain;

public class Device
{
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public string Brand { get; private set; }
    public DeviceState State { get; private set; }
    public DateTime CreationTime { get; private init; }

    public static ErrorOr<Device> Create(string name, string brand, DeviceState state = DeviceState.Inactive)
    {
        var nameResult = ValidateName(name);
        var brandResult = ValidateBrand(brand);

        if (nameResult.IsError || brandResult.IsError)
        {
            return nameResult.ErrorsOrEmptyList
                .Concat(brandResult.ErrorsOrEmptyList)
                .ToList();
        }

        return new Device
        {
            Id = Guid.NewGuid(),
            Name = name,
            Brand = brand,
            State = state,
            CreationTime = DateTime.UtcNow
        };
    }

    public ErrorOr<Success> UpdateName(string name)
    {
        if (State == DeviceState.InUse)
            return DeviceErrors.CannotUpdateNameInUse;

        var result = ValidateName(name);
        if (result.IsError) return result.Errors;

        Name = name;
        return Result.Success;
    }

    public ErrorOr<Success> UpdateBrand(string brand)
    {
        if (State == DeviceState.InUse)
            return DeviceErrors.CannotUpdateBrandInUse;

        var result = ValidateBrand(brand);
        if (result.IsError) return result.Errors;

        Brand = brand;
        return Result.Success;
    }

    public ErrorOr<Success> UpdateState(DeviceState newState)
    {
        State = newState;
        return Result.Success;
    }

    public ErrorOr<Deleted> Delete()
    {
        if (State == DeviceState.InUse)
        {
            return DeviceErrors.CannotDeleteInUse;
        }

        return Result.Deleted;
    }

    private static ErrorOr<Success> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return DeviceErrors.NameRequired;

        return Result.Success;
    }

    private static ErrorOr<Success> ValidateBrand(string brand)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(brand))
            errors.Add(DeviceErrors.BrandRequired);

        if (brand?.Length >= 15)
            errors.Add(DeviceErrors.BrandTooLong);

        return errors.Count > 0 ? errors : Result.Success;
    }

#pragma warning disable CS8618
    private Device() { /* For EF Core */ }
#pragma warning restore CS8618
}