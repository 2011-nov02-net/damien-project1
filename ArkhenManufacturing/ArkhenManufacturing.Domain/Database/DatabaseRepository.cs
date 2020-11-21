using System;
using System.Collections.Generic;

using ArkhenManufacturing.Library;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;

namespace ArkhenManufacturing.Domain.Database
{
    public class DatabaseRepository : IRepository
    {
        // dbcontext

        public DatabaseRepository(string connectionString) {

        }

        public bool Any<T>() where T : ArkhEntity {
            throw new NotImplementedException();
        }

        public void Create<T>(IData data) where T : ArkhEntity, new() {
            throw new NotImplementedException();
        }

        public void Delete<T>(Guid id) where T : ArkhEntity {
            throw new NotImplementedException();
        }

        public bool Exists<T>(Guid id) where T : ArkhEntity {
            throw new NotImplementedException();
        }

        public IData Retrieve<T>(Guid id) where T : ArkhEntity {
            throw new NotImplementedException();
        }

        public List<IData> RetrieveAll<T>() where T : ArkhEntity {
            throw new NotImplementedException();
        }

        public List<IData> RetrieveByName<T>(string name) where T : NamedArkhEntity {
            throw new NotImplementedException();
        }

        public void Update<T>(Guid id, IData data) where T : ArkhEntity {
            throw new NotImplementedException();
        }
    }
}
