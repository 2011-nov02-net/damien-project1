using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArkhenManufacturing.DataAccess;
using ArkhenManufacturing.Domain.Database.DbSetInterfacer;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using Microsoft.EntityFrameworkCore;

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
                { typeof(Admin), new AdminDbSetInterfacer(createContext) },
                { typeof(Customer), new CustomerDbSetInterfacer(createContext)},
                { typeof(InventoryEntry), new InventoryEntryDbSetInterfacer(createContext)},
                { typeof(Location), new LocationDbSetInterfacer(createContext)},
                { typeof(Order), new OrderDbSetInterfacer(createContext)},
                { typeof(OrderLine), new OrderLineDbSetInterfacer(createContext)},
                { typeof(Product), new ProductDbSetInterfacer(createContext)},
            };
        }

        private IDbSetInterfacer<T> Interfacer<T>()
            where T : ArkhEntity 
            => _interfacers[typeof(T)] as IDbSetInterfacer<T>;

        public bool Any<T>()
            where T : ArkhEntity
            => Interfacer<T>()
                .AnyAsync().Result;

        public async Task<bool> AnyAsync<T>()
            where T : ArkhEntity
            => await Task.Run(() => Any<T>());

        public bool Exists<T>(Guid id) 
            where T : ArkhEntity
            => Interfacer<T>()
                .ExistsAsync(id).Result;

        public async Task<bool> ExistsAsync<T>(Guid id)
            where T : ArkhEntity
            => await Task.Run(() => Exists<T>(id));

        public int Count<T>()
            where T : ArkhEntity
            => Interfacer<T>()
                .CountAsync().Result;

        public async Task<int> CountAsync<T>()
            where T : ArkhEntity
            => await Task.Run(() => Count<T>());

        public Guid Create<T>(IData data)
            where T : ArkhEntity, new()
            => Interfacer<T>()
                .CreateAsync(data).Result;

        public async Task<Guid> CreateAsync<T>(IData data)
            where T : ArkhEntity, new()
            => await Task.Run(() => Create<T>(data));

        public void Delete<T>(Guid id)
            where T : ArkhEntity
            => Interfacer<T>()
                .DeleteAsync(id);

        public async Task DeleteAsync<T>(Guid id)
            where T : ArkhEntity
            => await Task.Run(() => Delete<T>(id));

        public T Retrieve<T>(Guid id)
            where T : ArkhEntity
            => Interfacer<T>()
                .RetrieveAsync(id).Result;

        public async Task<T> RetrieveAsync<T>(Guid id)
            where T : ArkhEntity
            => await Task.Run(() => Retrieve<T>(id));

        public ICollection<T> RetrieveSome<T>(ICollection<Guid> ids)
            where T : ArkhEntity
            => Interfacer<T>()
                .RetrieveSomeAsync(ids).Result;

        public async Task<ICollection<T>> RetrieveSomeAsync<T>(ICollection<Guid> ids)
            where T : ArkhEntity
            => await Task.Run(() => Interfacer<T>()
                .RetrieveSomeAsync(ids));

        public List<T> RetrieveAll<T>()
            where T : ArkhEntity
            => Interfacer<T>()
                .RetrieveAllAsync().Result
                .ToList();

        public async Task<List<T>> RetrieveAllAsync<T>()
            where T : ArkhEntity
            => await Task.Run(() => RetrieveAll<T>());

        public List<T> RetrieveByName<T>(string name)
            where T : NamedArkhEntity
            => Interfacer<T>()
                .RetrieveAllAsync().Result
                .Where(item => item.GetName()
                    .Contains(name))
                .ToList();

        public async Task<List<T>> RetrieveByNameAsync<T>(string name)
            where T : NamedArkhEntity
            => await Task.Run(() => RetrieveByName<T>(name));

        public void Update<T>(Guid id, IData data)
            where T : ArkhEntity
            => Interfacer<T>()
                .UpdateAsync(id, data);

        public async Task UpdateAsync<T>(Guid id, IData data)
            where T : ArkhEntity
            => await Task.Run(() => Update<T>(id, data));
    }
}
