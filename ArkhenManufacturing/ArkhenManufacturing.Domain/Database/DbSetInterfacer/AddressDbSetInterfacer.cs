﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ArkhenManufacturing.DataAccess;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;

using Microsoft.EntityFrameworkCore;

namespace ArkhenManufacturing.Domain.Database.DbSetInterfacer
{
    public class AddressDbSetInterfacer : IDbSetInterfacer<Address>
    {
        private readonly Func<ArkhenContext> _createContext;

        public AddressDbSetInterfacer(Func<ArkhenContext> createContext) {
            _createContext = createContext;
        }

        public async Task<bool> AnyAsync() {
            using var context = _createContext();
            return await context.Addresses.AnyAsync();
        }

        public async Task<bool> ExistsAsync(Guid id) {
            using var context = _createContext();
            return await context.Addresses.FindAsync(id) is not null;
        }

        public async Task<int> CountAsync() {
            using var context = _createContext();
            return await context.Addresses.CountAsync();
        }

        public async Task<Guid> CreateAsync(IData data) {
            var dbAddress = DbEntityConverter.ToDbAddress(Guid.NewGuid(), data as AddressData);

            using (var context = _createContext()) {
                await context.Addresses.AddAsync(dbAddress);
                await context.SaveChangesAsync();
            }

            return dbAddress.Id;
        }

        public async Task<Address> RetrieveAsync(Guid id) {
            using var context = _createContext();
            var dbItem = await context.Addresses.FindAsync(id);
            return DbEntityConverter.ToAddress(dbItem);
        }

        public async Task<ICollection<Address>> RetrieveSomeAsync(ICollection<Guid> ids) {
            using var context = _createContext();
            return await context.Addresses
                .Where(a => ids.Contains(a.Id))
                .Select(a => DbEntityConverter.ToAddress(a))
                .ToListAsync();
        }

        public async Task<ICollection<Address>> RetrieveAllAsync() {
            using var context = _createContext();
            return await context.Addresses
                .Select(a => DbEntityConverter.ToAddress(a))
                .ToListAsync();
        }

        public async Task UpdateAsync(Guid id, IData data) {
            using var context = _createContext();
            var addresses = context.Addresses;

            if (addresses.Find(id) is not null) {
                addresses.Update(DbEntityConverter.ToDbAddress(id, data as AddressData));
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id) {
            using var context = _createContext();
            var addresses = context.Addresses;

            if (addresses.Find(id) is DbAddress dbAddress) {
                addresses.Remove(dbAddress);
                await context.SaveChangesAsync();
            }
        }

    }
}
