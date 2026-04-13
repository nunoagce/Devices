using Application;
using Domain;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DeviceRepository : IDeviceRepository
{
    private readonly AppDbContext _context;

    public DeviceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Device value)
    {
        await _context.AddAsync(value);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Device>> GetAllAsync()
    {
        return await _context.Devices.AsNoTracking().ToListAsync();
    }

    public async Task<Device?> GetByIdAsync(Guid id)
    {
        return await _context.Devices.FindAsync(id);
    }

    public async Task UpdateAsync(Device device)
    {
        _context.Update(device);
        await _context.SaveChangesAsync();
    }
}