using Microsoft.EntityFrameworkCore;
using RSVP.Core.Interfaces;
using RSVP.Core.Interfaces.Repositories;
using RSVP.Core.Models;
using RSVP.Infrastructure.Data;

namespace RSVP.Infrastructure.Repositories;

public class StoreRepository : Repository<Store>, IStoreRepository
{
    public StoreRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Store?> GetByStoreIdAsync(string storeId)
    {

        return await _dbSet.FirstOrDefaultAsync(s => s.StoreId == storeId);
    }

    public async Task<IEnumerable<Store>> GetStoresByLocationAsync(string location)
    {
        return await _dbSet
            .Where(s => s.Location.Contains(location))
            .ToListAsync();
    }

    public async Task<IEnumerable<Store>> GetStoresWithServicesAsync()
    {
        return await _dbSet
            .Include(s => s.StoreServices)
            .ThenInclude(s => s.Service)
            .ToListAsync();
    }

    public async Task<IEnumerable<Store>> GetStoresWithHoursAsync()
    {
        return await _dbSet
            .Include(s => s.StoreHour)
            .ToListAsync();
    }

    // public async Task<IEnumerable<Store>> GetAllStoresWithHoursAndServicesAsync()
    // {
    //     return await _dbSet
    //         .Include(s => s.StoreHour)
    //         .Include(s => s.StoreServices)
    //         .ThenInclude(s => s.Service)
    //         .ToListAsync();
    // }
    public async Task<bool> ExistsByStoreIdAsync(string storeId)
    {
        return await _dbSet.AnyAsync(s => s.StoreId == storeId);
    }
}