using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ArkhenManufacturing.DataAccess;
using ArkhenManufacturing.Domain.Database.DbSetInterfacer;
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
        private readonly Dictionary<Type, IInterfacer> _interfacers;
        
        public DatabaseRepository(DbContextOptions<ArkhenContext> options)
        {
            _options = options;

            ArkhenContext createContext() => new ArkhenContext(_options);

            _interfacers = new Dictionary<Type, IInterfacer>
            {
                { typeof(Address), new AddressDbSetInterfacer(createContext) },
                { typeof(Admin), null },
                { typeof(Customer), null },
                { typeof(InventoryEntry), null },
                { typeof(Location), null },
                { typeof(Order), null },
                { typeof(OrderLine), null },
                { typeof(Product), null },
            };
        }

        private IDbSetInterfacer<T> Interfacer<T>()
            where T : ArkhEntity 
            => _interfacers[typeof(T)] as IDbSetInterfacer<T>;

        public bool Any<T>()
            where T : ArkhEntity
            => Interfacer<T>()
                .Any();

        public async Task<bool> AnyAsync<T>()
            where T : ArkhEntity
            => await Task.Run(() => Any<T>());

        public bool Exists<T>(Guid id) 
            where T : ArkhEntity
            => Interfacer<T>()
                .Exists(id);

        public async Task<bool> ExistsAsync<T>(Guid id)
            where T : ArkhEntity
            => await Task.Run(() => Exists<T>(id));

        public List<T> RetrieveByName<T>(string name) 
            where T : NamedArkhEntity
            => Interfacer<T>()
                .RetrieveAll()
                .Where(item => item.GetName()
                    .Contains(name))
                .ToList();

        public async Task<List<T>> RetrieveByNameAsync<T>(string name)
            where T : NamedArkhEntity
            => await Task.Run(() => RetrieveByName<T>(name));

        public int Count<T>()
            where T : ArkhEntity
            => Interfacer<T>()
                .Count();

        public async Task<int> CountAsync<T>()
            where T : ArkhEntity
            => await Task.Run(() => Count<T>());

        public Guid Create<T>(IData data) 
            where T : ArkhEntity, new() 
            => Interfacer<T>()
                .Create(data);

        public async Task<Guid> CreateAsync<T>(IData data)
            where T : ArkhEntity, new()
            => await Task.Run(() => Create<T>(data));

        public void Delete<T>(Guid id)
            where T : ArkhEntity
            => Interfacer<T>()
                .Delete(id);

        public async Task DeleteAsync<T>(Guid id)
            where T : ArkhEntity
            => await Task.Run(() => Delete<T>(id));

        public List<T> RetrieveAll<T>()
            where T : ArkhEntity
            => Interfacer<T>()
                .RetrieveAll()
                .ToList();

        public async Task<List<T>> RetrieveAllAsync<T>()
            where T : ArkhEntity
            => await Task.Run(() => RetrieveAll<T>());

        public T Retrieve<T>(Guid id)
            where T : ArkhEntity
            => Interfacer<T>()
                .Retrieve(id);

        public async Task<T> RetrieveAsync<T>(Guid id)
            where T : ArkhEntity
            => await Task.Run(() => Retrieve<T>(id));

        public void Update<T>(Guid id, IData data)
            where T : ArkhEntity
            => Interfacer<T>()
                .Update(id, data);

        public async Task UpdateAsync<T>(Guid id, IData data)
            where T : ArkhEntity
            => await Task.Run(() => Update<T>(id, data));
    }
}
