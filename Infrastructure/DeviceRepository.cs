using Application;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DeviceRepository : IDeviceRepository
{
    private readonly AppDbContext _context;

    public DeviceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Device>> GetAllAsync()
    {
        return await _context.Devices.ToListAsync();
    }
}