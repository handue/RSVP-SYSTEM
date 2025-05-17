using Microsoft.EntityFrameworkCore;
using RSVP.Core.Interfaces;
using RSVP.Core.Interfaces.Repositories;
using RSVP.Core.Models;
using RSVP.Infrastructure.Data;

namespace RSVP.Infrastructure.Repositories;

public class ServiceRepository : Repository<Service>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Service?> GetByServiceIdAsync(string serviceId)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.ServiceId == serviceId);
    }

    public async Task<IEnumerable<Service>> GetServicesByStoreIdAsync(string storeId)
    {
        return await _dbSet
            .Where(s => s.StoreId == storeId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Service>> GetServicesWithReservationsAsync()
    {
        return await _dbSet
            .Include(s => s.Reservations)
            .ToListAsync();
    }

    public async Task<bool> ExistsByServiceIdAsync(string serviceId)
    {
        return await _dbSet.AnyAsync(s => s.ServiceId == serviceId);
    }
} 