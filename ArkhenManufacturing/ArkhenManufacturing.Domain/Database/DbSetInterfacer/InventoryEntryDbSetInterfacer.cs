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
    public class InventoryEntryDbSetInterfacer : IDbSetInterfacer<InventoryEntry>
    {
        private readonly Func<ArkhenContext> _createContext;

        public InventoryEntryDbSetInterfacer(Func<ArkhenContext> createContext) {
            _createContext = createContext;
        }

        public async Task<bool> AnyAsync() {
            using var context = _createContext();
            return await context.InventoryEntries.AnyAsync();
        }

        public async Task<bool> ExistsAsync(Guid id) {
            using var context = _createContext();
            return await context.InventoryEntries.FirstOrDefaultAsync(ie => ie.Id == id) is not null;
        }

        public async Task<int> CountAsync() {
            using var context = _createContext();
            return await context.InventoryEntries.CountAsync();
        }

        public async Task<Guid> CreateAsync(IData data) {
            var dbInventoryEntry = DbEntityConverter.ToDbInventoryEntry(Guid.NewGuid(), data as InventoryEntryData);

            using (var context = _createContext()) {
                await context.InventoryEntries.AddAsync(dbInventoryEntry);
                await context.SaveChangesAsync();
            }

            return dbInventoryEntry.Id;
        }

        public async Task<InventoryEntry> RetrieveAsync(Guid id) {
            using var context = _createContext();
            var dbInventoryEntry = await context.InventoryEntries.FirstOrDefaultAsync(ie => ie.Id == id);
            return DbEntityConverter.ToInventoryEntry(dbInventoryEntry);
        }

        public async Task<ICollection<InventoryEntry>> RetrieveSomeAsync(ICollection<Guid> ids) {
            using var context = _createContext();

            if(!await context.InventoryEntries.AnyAsync()) {
                return new List<InventoryEntry>();
            }

            return await context.InventoryEntries
                .Where(ie => ids.Contains(ie.Id))
                .Select(ie => DbEntityConverter.ToInventoryEntry(ie))
                .ToListAsync();
        }

        public async Task<ICollection<InventoryEntry>> RetrieveAllAsync() {
            using var context = _createContext();

            if(!await context.InventoryEntries.AnyAsync()) {
                return new List<InventoryEntry>();
            }

            return await context.InventoryEntries
                .Select(ie => DbEntityConverter.ToInventoryEntry(ie))
                .ToListAsync();
        }

        public async Task UpdateAsync(Guid id, IData data) {
            using var context = _createContext();
            var inventoryEntries = context.InventoryEntries;

            var inventoryEntryData = data as InventoryEntryData;

            if(await inventoryEntries.FirstOrDefaultAsync(ie => ie.Id == id) is DbInventoryEntry dbInventoryEntry) {
                dbInventoryEntry.ProductId = inventoryEntryData.ProductId;
                dbInventoryEntry.Price = inventoryEntryData.Price;
                dbInventoryEntry.Count = inventoryEntryData.Count;
                dbInventoryEntry.Discount = inventoryEntryData.Discount;
                dbInventoryEntry.LocationId = inventoryEntryData.LocationId;
                dbInventoryEntry.Threshold = inventoryEntryData.Threshold;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id) {
            using var context = _createContext();
            var inventoryEntries = context.InventoryEntries;

            if(await inventoryEntries.FirstOrDefaultAsync(ie => ie.Id == id) is DbInventoryEntry dbInventoryEntry) {
                inventoryEntries.Remove(dbInventoryEntry);
                await context.SaveChangesAsync();
            }
        }
    }
}
