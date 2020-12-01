using System;
using System.Collections.Generic;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;

namespace ArkhenManufacturing.Domain.Database.DbSetInterfacer
{
    public class AdminDbSetInterfacer : IDbSetInterfacer<Admin>
    {
        public AdminDbSetInterfacer() {

        }

        public bool Any() {
            throw new NotImplementedException();
        }

        public int Count() {
            throw new NotImplementedException();
        }

        public Guid Create(IData data) {
            throw new NotImplementedException();
        }

        public void Delete(Guid id) {
            throw new NotImplementedException();
        }

        public bool Exists(Guid id) {
            throw new NotImplementedException();
        }

        public Admin Retrieve(Guid id) {
            throw new NotImplementedException();
        }

        public ICollection<Admin> RetrieveAll() {
            throw new NotImplementedException();
        }

        public void Update(Guid id, IData data) {
            throw new NotImplementedException();
        }
    }
}
