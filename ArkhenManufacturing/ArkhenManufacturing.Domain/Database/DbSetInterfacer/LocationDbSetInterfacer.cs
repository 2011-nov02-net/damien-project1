using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ArkhenManufacturing.DataAccess;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;

using Microsoft.EntityFrameworkCore;

namespace ArkhenManufacturing.Domain.Database.DbSetInterfacer
{
    public class LocationDbSetInterfacer : IDbSetInterfacer<Location>
    {
        private readonly Func<ArkhenContext> _createContext;

        public LocationDbSetInterfacer(Func<ArkhenContext> createContext) {
            _createContext = createContext;
        }

        public async Task<bool> AnyAsync() {
            using var context = _createContext();
            return await context.Locations.AnyAsync();
        }

        public async Task<bool> ExistsAsync(Guid id) {
            using var context = _createContext();
            return await context.Locations.FirstOrDefaultAsync(l => l.Id == id) is not null;
        }

        public async Task<int> CountAsync() {
            using var context = _createContext();
            return await context.Locations.CountAsync();
        }

        public async Task<Guid> CreateAsync(IData data) {
            var dbLocation = DbEntityConverter.ToDbLocation(Guid.NewGuid(), data as LocationData);

            using (var context = _createContext()) {
                await context.Locations.AddAsync(dbLocation);
                await context.SaveChangesAsync();
            }

            return dbLocation.Id;
        }

        public async Task<Location> RetrieveAsync(Guid id) {
            using var context = _createContext();
            var dbLocation = await context.Locations.FirstOrDefaultAsync(l => l.Id == id);
            return DbEntityConverter.ToLocation(dbLocation);
        }

        public async Task<ICollection<Location>> RetrieveSomeAsync(ICollection<Guid> ids) {
            using var context = _createContext();

            if(!await context.Locations.AnyAsync()) {
                return new List<Location>();
            }

            return await context.Locations
                .Where(ie => ids.Contains(ie.Id))
                .Select(ie => DbEntityConverter.ToLocation(ie))
                .ToListAsync();
        }

        public async Task<ICollection<Location>> RetrieveAllAsync() {
            using var context = _createContext();

            if (!await context.Locations.AnyAsync()) {
                return new List<Location>();
            }

            return await context.Locations
                .Select(ie => DbEntityConverter.ToLocation(ie))
                .ToListAsync();
        }

        public async Task UpdateAsync(Guid id, IData data) {
            using var context = _createContext();
            var locations = context.Locations;

            var locationData = data as LocationData;

            if (await locations.FirstOrDefaultAsync(l => l.Id == id) is DbLocation dbLocation) {
                dbLocation.Name = locationData.Name;
                dbLocation.AddressId = locationData.AddressId;

                dbLocation.Admins = await context.Admins
                    .Where(a => locationData.AdminIds.Contains(a.Id))
                    .ToListAsync();

                dbLocation.Orders = await context.Orders
                    .Where(o => locationData.OrderIds.Contains(o.Id))
                    .ToListAsync();

                dbLocation.InventoryEntries = await context.InventoryEntries
                    .Where(ie => locationData.InventoryEntryIds.Contains(ie.Id))
                    .ToListAsync();

                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id) {
            using var context = _createContext();
            var locations = context.Locations;

            if (await locations.FirstOrDefaultAsync(l => l.Id == id) is DbLocation dbLocation) {
                locations.Remove(dbLocation);
                await context.SaveChangesAsync();
            }
        }
    }
}
