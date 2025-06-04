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

    public async Task<IEnumerable<Store>> SaveAllAsync(IEnumerable<Store> stores)
    {
        foreach (var store in stores)
        {
            var existingStore = await _dbSet
                .Include(s => s.StoreHour)
                    .ThenInclude(sh => sh.RegularHours)
                .Include(s => s.StoreHour)
                    .ThenInclude(sh => sh.SpecialDate)
                .FirstOrDefaultAsync(s => s.StoreId == store.StoreId);

            if (existingStore != null)
            {
                // original entity update
                // what's the difference between Update and CurrentValues.SetValues? 
                // ************************************************************ //
                // Update()와 CurrentValues.SetValues()의 차이점:
                // 1. Update()는 엔티티를 "수정됨" 상태로 표시하고 변경 추적을 시작합니다.
                //    SetValues()는 단순히 속성값만 업데이트하고 변경 추적을 하지 않습니다.
                // 2. SaveChangesAsync()가 이미 변경 추적을 수행하므로, 
                //    SetValues()를 사용하는 것이 더 효율적입니다. (여기서 update하고 아래에서 SaveChangesAsync()를 또 하면 이중 추적돼서 오류 발생)
                //
                // Difference between Update() and CurrentValues.SetValues():
                // 1. Update() marks the entity as "Modified" and starts change tracking.
                //    SetValues() only updates property values without change tracking.
                // 2. Since SaveChangesAsync() already performs change tracking,
                //    using SetValues() is more efficient.
                // ************************************************************ //
                _context.Entry(existingStore).CurrentValues.SetValues(store);

                // * .Net doesn't support update navigation property with Include, so we need to update each navigation property separately
                // * update whole part is inefficient and may cause data integrity or conflict avoidance issues, so we need to update each navigation property separately

                // * 무작정 전체 업데이트를 하면 비효율적이고 데이터 무결성이나 충돌 방지 때문에 이렇게 수동적으로 나눠서 해야한다고 하네요.

                if (store.StoreHour != null && existingStore.StoreHour != null)
                {
                    _context.Entry(existingStore.StoreHour).CurrentValues.SetValues(store.StoreHour);


                    _context.RemoveRange(existingStore.StoreHour.RegularHours);

                    foreach (var regularHour in store.StoreHour.RegularHours)
                    {
                        // regularHour.StoreHourId = existingStore.StoreHour.Id;
                        existingStore.StoreHour.RegularHours.Add(regularHour);
                    }

                    if (existingStore.StoreHour.SpecialDate?.Any() == true)
                    {
                        _context.RemoveRange(existingStore.StoreHour.SpecialDate);
                    }

                    if (store.StoreHour.SpecialDate?.Any() == true)
                    {
                        existingStore.StoreHour.SpecialDate = new List<SpecialDate>();
                        foreach (var specialDate in store.StoreHour.SpecialDate)
                        {
                            // specialDate.StoreHourId = existingStore.StoreHour.StoreHourId;
                            existingStore.StoreHour.SpecialDate.Add(specialDate);
                        }
                    }


                }


            }
            else
            {
                // new entity add
                await _dbSet.AddAsync(store);
            }
        }

        await SaveChangesAsync();
        return stores;
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