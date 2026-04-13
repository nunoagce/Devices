using Domain;
using ErrorOr;

namespace Application;

public interface IDeviceRepository
{
    Task AddAsync(Device value);
    Task<List<Device>> GetAllAsync();
    Task<Device?> GetById(Guid id);
}
