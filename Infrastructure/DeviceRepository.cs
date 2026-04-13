using Application;
using Domain;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System.Threading;

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

    public async Task RemoveAsync(Device device)
    {
        _context.Remove(device);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Device>> ListAsync(string? brand = null, DeviceState? state = null)
    {
        var query = _context.Devices.AsQueryable();

        if (!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(d => d.Brand == brand);
        }

        if (state.HasValue)
        {
            query = query.Where(d => d.State == state.Value);
        }

        return await query.AsNoTracking().ToListAsync();
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