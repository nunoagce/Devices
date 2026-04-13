using Domain;
using ErrorOr;

namespace Application;

public interface IDeviceRepository
{
    Task AddAsync(Device value);
    Task RemoveAsync(Device device);
    Task<List<Device>> GetAllAsync();
    Task<Device?> GetByIdAsync(Guid id);
    Task UpdateAsync(Device device);
}
