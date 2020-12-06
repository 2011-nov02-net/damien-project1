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
    public class OrderLineDbSetInterfacer : IDbSetInterfacer<OrderLine>
    {
        private readonly Func<ArkhenContext> _createContext;

        public OrderLineDbSetInterfacer(Func<ArkhenContext> createContext) {
            _createContext = createContext;
        }

        public async Task<bool> AnyAsync() {
            using var context = _createContext();
            return await context.OrderLines.AnyAsync();
        }

        public async Task<bool> ExistsAsync(Guid id) {
            using var context = _createContext();
            return await context.OrderLines.FirstOrDefaultAsync(ol => ol.Id == id) is not null;
        }

        public async Task<int> CountAsync() {
            using var context = _createContext();
            return await context.OrderLines.CountAsync();
        }

        public async Task<Guid> CreateAsync(IData data) {
            var dbOrderLine = DbEntityConverter.ToDbOrderLine(Guid.NewGuid(), data as OrderLineData);

            using (var context = _createContext()) {
                await context.OrderLines.AddAsync(dbOrderLine);
                await context.SaveChangesAsync();
            }

            return dbOrderLine.Id;
        }

        public async Task<OrderLine> RetrieveAsync(Guid id) {
            using var context = _createContext();
            var dbOrderLine = await context.OrderLines.FirstOrDefaultAsync(ol => ol.Id == id);
            return DbEntityConverter.ToOrderLine(dbOrderLine);
        }

        public async Task<ICollection<OrderLine>> RetrieveSomeAsync(ICollection<Guid> ids) {
            using var context = _createContext();

            if (!await context.OrderLines.AnyAsync()) {
                return new List<OrderLine>();
            }

            return await context.OrderLines
                .Where(ie => ids.Contains(ie.Id))
                .Select(ie => DbEntityConverter.ToOrderLine(ie))
                .ToListAsync();
        }

        public async Task<ICollection<OrderLine>> RetrieveAllAsync() {
            using var context = _createContext();

            if (!await context.OrderLines.AnyAsync()) {
                return new List<OrderLine>();
            }

            return await context.OrderLines
                .Select(ie => DbEntityConverter.ToOrderLine(ie))
                .ToListAsync();
        }

        public async Task UpdateAsync(Guid id, IData data) {
            using var context = _createContext();
            var orderLines = context.OrderLines;

            if (await orderLines.FirstOrDefaultAsync(ol => ol.Id == id) is not null) {
                orderLines.Update(DbEntityConverter.ToDbOrderLine(id, data as OrderLineData));
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id) {
            using var context = _createContext();
            var orderLines = context.OrderLines;

            if (await orderLines.FirstOrDefaultAsync(ol => ol.Id == id) is DbOrderLine dbOrderLine) {
                orderLines.Remove(dbOrderLine);
                await context.SaveChangesAsync();
            }
        }
    }
}
