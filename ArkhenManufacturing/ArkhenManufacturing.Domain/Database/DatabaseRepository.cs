using System;
using System.Collections.Generic;

using ArkhenManufacturing.DataAccess;
using ArkhenManufacturing.Library;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.Library.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArkhenManufacturing.Domain.Database
{
    public class DatabaseRepository : IRepository
    {
        private readonly DbContextOptions<ArkhenContext> _options;
        
        public DatabaseRepository(DbContextOptions<ArkhenContext> options)
        {
            _options = options;
        }

        public bool Any<T>() where T : ArkhEntity {
            throw new NotImplementedException();
        }

        public bool Exists<T>(Guid id) where T : ArkhEntity {
            throw new NotImplementedException();
        }

        public List<T> RetrieveByName<T>(string name) where T : NamedArkhEntity {
            throw new NotImplementedException();
        }

        public int Count<T>() where T : ArkhEntity {
            throw new NotImplementedException();
        }

        public Guid Create<T>(IData data) where T : ArkhEntity, new() {
            throw new NotImplementedException();
        }

        public void Delete<T>(Guid id) where T : ArkhEntity {
            throw new NotImplementedException();
        }

        public List<T> RetrieveAll<T>() where T : ArkhEntity {
            throw new NotImplementedException();
        }

        public T Retrieve<T>(Guid id) where T : ArkhEntity {
            throw new NotImplementedException();
        }

        public void Update<T>(Guid id, IData data) where T : ArkhEntity {
            throw new NotImplementedException();
        }
    }
}
