using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool Any() {
            using var context = _createContext();
            return context.Addresses.Any();
        }

        public bool Exists(Guid id) {
            using var context = _createContext();
            return context.Addresses.Find(id) is not null;
        }

        public int Count() {
            using var context = _createContext();
            return context.Addresses.Count();
        }

        public void Create(IData data) {
            var item = DbEntityConverter.ToDbAddress(Guid.NewGuid(), data as AddressData);

            using (var context = _createContext()) {
                context.Addresses.Add(item);
                context.SaveChanges();
            }
        }

        public ICollection<Address> RetrieveAll() {
            using var context = _createContext();
            return context.Addresses.ToList()
                .ConvertAll(a => new Address(a.Id, DbEntityConverter.ToAddress(a)));
        }

        public Address Retrieve(Guid id) {
            using var context = _createContext();
            var dbItem = context.Addresses.Find(id);
            var item = new Address(id, DbEntityConverter.ToAddress(dbItem));
            return item;
        }

        public void Update(Guid id, IData data) {
            var item = DbEntityConverter.ToDbAddress(id, data as AddressData);
            using (var context = _createContext()) {
                context.Addresses.Update(item);
            }
        }

        public void Delete(Guid id) {
            var data = Retrieve(id).GetData() as AddressData;
            var item = DbEntityConverter.ToDbAddress(id, data);

            using (var context = _createContext()) {
                context.Addresses.Remove(item);
                context.SaveChanges();
            }
        }

    }
}
