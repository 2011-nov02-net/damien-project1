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
    public class OrderDbSetInterfacer : IDbSetInterfacer<Order>
    {
        private readonly Func<ArkhenContext> _createContext;

        public OrderDbSetInterfacer(Func<ArkhenContext> createContext) {
            _createContext = createContext;
        }

        public async Task<bool> AnyAsync() {
            using var context = _createContext();
            return await context.Orders.AnyAsync();
        }

        public async Task<bool> ExistsAsync(Guid id) {
            using var context = _createContext();
            return await context.Orders.FindAsync(id) is not null;
        }

        public async Task<int> CountAsync() {
            using var context = _createContext();
            return await context.Orders.CountAsync();
        }

        public async Task<Guid> CreateAsync(IData data) {
            var dbOrder = DbEntityConverter.ToDbOrder(Guid.NewGuid(), data as OrderData);

            using (var context = _createContext()) {
                await context.Orders.AddAsync(dbOrder);
                await context.SaveChangesAsync();
            }

            return dbOrder.Id;
        }

        public async Task<Order> RetrieveAsync(Guid id) {
            using var context = _createContext();
            var dbOrder = await context.Orders.FindAsync(id);
            return DbEntityConverter.ToOrder(dbOrder);
        }

        public async Task<ICollection<Order>> RetrieveSomeAsync(ICollection<Guid> ids) {
            using var context = _createContext();
            return await context.Orders
                .Where(ie => ids.Contains(ie.Id))
                .Select(ie => DbEntityConverter.ToOrder(ie))
                .ToListAsync();
        }

        public async Task<ICollection<Order>> RetrieveAllAsync() {
            using var context = _createContext();
            return await context.Orders
                .Select(ie => DbEntityConverter.ToOrder(ie))
                .ToListAsync();
        }

        public async Task UpdateAsync(Guid id, IData data) {
            using var context = _createContext();
            var orders = context.Orders;

            if (orders.Find(id) is not null) {
                orders.Update(DbEntityConverter.ToDbOrder(id, data as OrderData));
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id) {
            using var context = _createContext();
            var orders = context.Orders;

            if (orders.Find(id) is DbOrder dbOrder) {
                orders.Remove(dbOrder);
                await context.SaveChangesAsync();
            }
        }
    }
}
