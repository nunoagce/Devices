using Domain;

namespace Application;

public interface IDeviceRepository
{
    Task<List<Device>> GetAllAsync();
}
