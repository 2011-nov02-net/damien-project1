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
    public class ProductDbSetInterfacer : IDbSetInterfacer<Product>
    {
        private readonly Func<ArkhenContext> _createContext;

        public ProductDbSetInterfacer(Func<ArkhenContext> createContext) {
            _createContext = createContext;
        }

        public async Task<bool> AnyAsync() {
            using var context = _createContext();
            return await context.Products.AnyAsync();
        }

        public async Task<bool> ExistsAsync(Guid id) {
            using var context = _createContext();
            return await context.Products.FirstOrDefaultAsync(p => p.Id == id) is not null;
        }

        public async Task<int> CountAsync() {
            using var context = _createContext();
            return await context.Products.CountAsync();
        }

        public async Task<Guid> CreateAsync(IData data) {
            var dbProduct = DbEntityConverter.ToDbProduct(Guid.NewGuid(), data as ProductData);

            using (var context = _createContext()) {
                await context.Products.AddAsync(dbProduct);
                await context.SaveChangesAsync();
            }

            return dbProduct.Id;
        }

        public async Task<Product> RetrieveAsync(Guid id) {
            using var context = _createContext();
            var dbProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return DbEntityConverter.ToProduct(dbProduct);
        }

        public async Task<ICollection<Product>> RetrieveSomeAsync(ICollection<Guid> ids) {
            using var context = _createContext();

            if (!await context.Products.AnyAsync()) {
                return new List<Product>();
            }

            return await context.Products
                .Where(ie => ids.Contains(ie.Id))
                .Select(ie => DbEntityConverter.ToProduct(ie))
                .ToListAsync();
        }

        public async Task<ICollection<Product>> RetrieveAllAsync() {
            using var context = _createContext();

            if (!await context.Products.AnyAsync()) {
                return new List<Product>();
            }

            return await context.Products
                .Select(ie => DbEntityConverter.ToProduct(ie))
                .ToListAsync();
        }

        public async Task UpdateAsync(Guid id, IData data) {
            using var context = _createContext();
            var products = context.Products;

            var productData = data as ProductData;

            if (await products.FirstOrDefaultAsync(p => p.Id == id) is DbProduct dbProduct) {
                dbProduct.Name = productData.Name;

                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id) {
            using var context = _createContext();
            var products = context.Products;

            if (!await products.AnyAsync()) {
                return;
            }

            if (await products.FirstOrDefaultAsync(p => p.Id == id) is DbProduct dbProduct) {
                products.Remove(dbProduct);
                await context.SaveChangesAsync();
            }
        }
    }
}
