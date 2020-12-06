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
    public class CustomerDbSetInterfacer : IDbSetInterfacer<Customer>
    {
        private readonly Func<ArkhenContext> _createContext;

        public CustomerDbSetInterfacer(Func<ArkhenContext> createContext) {
            _createContext = createContext;
        }

        public async Task<bool> AnyAsync() {
            using var context = _createContext();
            return await context.Customers.AnyAsync();
        }

        public async Task<bool> ExistsAsync(Guid id) {
            using var context = _createContext();
            return await context.Customers.FirstOrDefaultAsync(c => c.Id == id) is not null;
        }

        public async Task<int> CountAsync() {
            using var context = _createContext();
            return await context.Customers.CountAsync();
        }

        public async Task<Guid> CreateAsync(IData data) {
            var dbCustomer = DbEntityConverter.ToDbCustomer(Guid.NewGuid(), data as CustomerData);

            using (var context = _createContext()) {
                await context.Customers.AddAsync(dbCustomer);
                await context.SaveChangesAsync();
            }

            return dbCustomer.Id;
        }

        public async Task<Customer> RetrieveAsync(Guid id) {
            using var context = _createContext();
            var dbCustomer = await context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            return DbEntityConverter.ToCustomer(dbCustomer);
        }

        public async Task<ICollection<Customer>> RetrieveSomeAsync(ICollection<Guid> ids) {
            using var context = _createContext();

            if (!await context.Customers.AnyAsync()) {
                return new List<Customer>();
            }

            return await context.Customers
                .Where(c => ids.Contains(c.Id))
                .Select(c => DbEntityConverter.ToCustomer(c))
                .ToListAsync();
        }

        public async Task<ICollection<Customer>> RetrieveAllAsync() {
            using var context = _createContext();

            if (!await context.Customers.AnyAsync()) {
                return new List<Customer>();
            }

            return await context.Customers
                .Select(c => DbEntityConverter.ToCustomer(c))
                .ToListAsync();
        }

        public async Task UpdateAsync(Guid id, IData data) {
            using var context = _createContext();
            var customers = context.Customers;

            var customerData = data as CustomerData;

            if (await customers.FirstOrDefaultAsync(c => c.Id == id) is DbCustomer dbCustomer) {
                dbCustomer.FirstName = customerData.FirstName;
                dbCustomer.LastName = customerData.LastName;
                dbCustomer.PhoneNumber = customerData.PhoneNumber;
                dbCustomer.Email = customerData.Email;
                dbCustomer.SignUpDate = customerData.SignUpDate;
                dbCustomer.AddressId = customerData.AddressId;
                dbCustomer.BirthDate = customerData.BirthDate;
                dbCustomer.DefaultLocationId = customerData.DefaultLocationId;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id) {
            using var context = _createContext();
            var customers = context.Customers;

            if (await customers.FirstOrDefaultAsync(c => c.Id == id) is DbCustomer customer) {
                customers.Remove(customer);
                await context.SaveChangesAsync();
            }
        }
    }
}
