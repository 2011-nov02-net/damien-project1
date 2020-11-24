using System;
using System.Collections.Generic;

using ArkhenManufacturing.DataAccess;
using ArkhenManufacturing.Library;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.Library.Extensions;

using Microsoft.EntityFrameworkCore;

namespace ArkhenManufacturing.Domain.Database
{
    public class DatabaseRepository : IRepository
    {
        private readonly DbContextOptions<ArkhenContext> _options;
        private readonly ILogger _logger;

        public DatabaseRepository(string connectionString, ILogger logger) {
            _logger = logger ?? new FileLogger("arkhen_manufacturing.ef.log");
            connectionString.NullOrEmptyCheck(nameof(connectionString));
            var optionsBuilder = new DbContextOptionsBuilder<ArkhenContext>();
            optionsBuilder.UseSqlServer(connectionString);
            _options = optionsBuilder.Options;
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
