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
    public class AdminDbSetInterfacer : IDbSetInterfacer<Admin>
    {
        private readonly Func<ArkhenContext> _createContext;

        public AdminDbSetInterfacer(Func<ArkhenContext> createContext) {
            _createContext = createContext;
        }

        public async Task<bool> AnyAsync() {
            using var context = _createContext();
            return await context.Admins.AnyAsync();
        }

        public async Task<bool> ExistsAsync(Guid id) {
            using var context = _createContext();
            return await context.Admins.FindAsync(id) is not null;
        }

        public async Task<int> CountAsync() {
            using var context = _createContext();
            return await context.Admins.CountAsync();
        }

        public async Task<Guid> CreateAsync(IData data) {
            var dbAdmin = DbEntityConverter.ToDbAdmin(Guid.NewGuid(), data as AdminData);

            using (var context = _createContext()) {
                await context.Admins.AddAsync(dbAdmin);
                await context.SaveChangesAsync();
            }

            return dbAdmin.Id;
        }

        public async Task<Admin> RetrieveAsync(Guid id) {
            using var context = _createContext();
            var dbAdmin = await context.Admins.FindAsync(id);
            return DbEntityConverter.ToAdmin(dbAdmin);
        }

        public async Task<ICollection<Admin>> RetrieveSomeAsync(ICollection<Guid> ids) {
            using var context = _createContext();
            return await context.Admins
                .Where(a => ids.Contains(a.Id))
                .Select(a => DbEntityConverter.ToAdmin(a))
                .ToListAsync();
        }

        public async Task<ICollection<Admin>> RetrieveAllAsync() {
            using var context = _createContext();
            return await context.Admins
                .Select(a => DbEntityConverter.ToAdmin(a))
                .ToListAsync();
        }

        public async Task UpdateAsync(Guid id, IData data) {
            using var context = _createContext();
            var admins = context.Admins;

            if(admins.Find(id) is not null) {
                admins.Update(DbEntityConverter.ToDbAdmin(id, data as AdminData));
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id) {
            using var context = _createContext();
            var admins = context.Admins;

            if(admins.Find(id) is DbAdmin dbAdmin) {
                admins.Remove(dbAdmin);
                await context.SaveChangesAsync();
            }
        }
    }
}
