using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;

namespace ArkhenManufacturing.Domain.Internal
{
    /// <summary>
    /// The internal ephemeral repository that the application uses by default
    ///     if an IRepository is not specified
    /// </summary>
    public class InternalRepository : IRepository
    {
        private readonly Dictionary<Type, ICollection> _typeFactories;

        /// <summary>
        /// Constructor for this repository class; just sets up the FactoryManager
        /// </summary>
        public InternalRepository() {
            _typeFactories = new Dictionary<Type, ICollection>
            {
                { typeof(Address), new List<Address>() },
                { typeof(Admin), new List<Admin>() },
                { typeof(Customer), new List<Customer>() },
                { typeof(InventoryEntry), new List<InventoryEntry>() },
                { typeof(Location), new List<Location>() },
                { typeof(Order), new List<Order>() },
                { typeof(OrderLine), new List<OrderLine>() },
                { typeof(Product), new List<Product>() },
            };

            //byte[] data = Encoding.ASCII.GetBytes("Password123");
            //data = new SHA256Managed().ComputeHash(data);
            //string password = Encoding.ASCII.GetString(data);
            
            //// Product ids
            //Guid coffeeId = Create<Product>(new ProductData("Coffee"));
            //Guid waterBottleId = Create<Product>(new ProductData("Water Bottle"));
            //Guid usbDrive8GBId = Create<Product>(new ProductData("USB Drive (8GB)"));

            //// Address ids
            //Guid addressId = Create<Address>(new AddressData("123 Location Dr", "", "Eleison", "Khalrun", "Cylon-243", "12345"));
            //Guid damienAddressId = Create<Address>(new AddressData("123 Road Drive", "", "Eleison", "Khalrun", "Cylon-243", "12345"));

            //// Location ids
            //Guid arkhenplatzId = Create<Location>(new LocationData("Arkhenplatz", addressId, new List<Guid>(), new List<Guid>(), new List<Guid>()));

            //// Admin id
            //Guid veroAdminId = Create<Admin>(new AdminData("Vero", "Richter", "vero.richter@arkhen.net", arkhenplatzId));

            //// Add the admin id to the location
            //var location = Retrieve<Location>(arkhenplatzId);
            //(location.GetData() as LocationData).AdminIds.Add(veroAdminId);

            //// Inventory Entry ids
            //Create<InventoryEntry>(new InventoryEntryData(coffeeId, arkhenplatzId, 10.00M, 0.00M, 10, 2));
            //Create<InventoryEntry>(new InventoryEntryData(waterBottleId, arkhenplatzId, 1.00M, 0.00M, 24, 2));
            //Create<InventoryEntry>(new InventoryEntryData(usbDrive8GBId, arkhenplatzId, 12.00M, 0.00M, 5, 1));

            //// Customer ids
            //Create<Customer>(new CustomerData("Damien", "Bevins", "damien.bevins@outlook.com", "(385) 271-8623", damienAddressId, DateTime.Now, new DateTime(1997, 7, 23), arkhenplatzId));
        }

        /// <summary>
        /// A utility method that eases the retrieval of a certain kind of factory
        /// </summary>
        /// <typeparam name="T">The subclass of ArkhEntity being targeted</typeparam>
        /// <returns>An ICollection of the specified subclass</returns>
        private ICollection<T> GetList<T>()
            where T : ArkhEntity => _typeFactories[typeof(T)] as ICollection<T>;

        /// <summary>
        /// Method that checks for any ArkhEntities within the collection
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <returns>True if any are present, otherwise returns false</returns>
        public bool Any<T>()
            where T : ArkhEntity => GetList<T>().Any();

        public async Task<bool> AnyAsync<T>()
            where T : ArkhEntity => await Task.Run(() => Any<T>());

        /// <summary>
        /// Checks for an ArkhEntity with the specified Guid
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        /// <returns>An ArkhEntity, if any was found. If it wasn't found, return null</returns>
        public bool Exists<T>(Guid id) 
            where T : ArkhEntity {
            var list = GetList<T>();
            return list.Any() && list
                .Select(item => item.Id)
                .Contains(id);
        }

        public async Task<bool> ExistsAsync<T>(Guid id)
            where T : ArkhEntity => await Task.Run(() => Exists<T>(id));

        /// <summary>
        /// Gets the count in a collection
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity in the collection</typeparam>
        /// <returns>An unsigned int of the number in the collection</returns>
        public int Count<T>()
            where T : ArkhEntity => GetList<T>().Count;

        public async Task<int> CountAsync<T>()
            where T : ArkhEntity => await Task.Run(() => Count<T>());

        /// <summary>
        /// Create an ArkhEntity child with the specified data
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="data">The specific data type being passed in</param>
        public Guid Create<T>(IData data) 
            where T : ArkhEntity, new() {
            var item = new T();
            item.SetData(data);
            GetList<T>().Add(item);
            return item.Id;
        }

        public async Task<Guid> CreateAsync<T>(IData data)
            where T : ArkhEntity, new() => await Task.Run(() => Create<T>(data));

        /// <summary>
        /// Get an item using a Guid id
        ///     If it doesn't exist, it returns null
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the item</param>
        /// <returns>The item that has the specified Guid, otherwise it returns null</returns>
        public T Retrieve<T>(Guid id)
            where T : ArkhEntity => GetList<T>()
                .FirstOrDefault(item => item.Id == id);

        public async Task<T> RetrieveAsync<T>(Guid id)
            where T : ArkhEntity => await Task.Run(() => Retrieve<T>(id));

        public ICollection<T> RetrieveSome<T>(ICollection<Guid> ids)
            where T : ArkhEntity => GetList<T>()
                .Where(t => ids.Contains(t.Id))
                .ToList();

        public async Task<ICollection<T>> RetrieveSomeAsync<T>(ICollection<Guid> ids)
            where T : ArkhEntity => await Task.Run(() => RetrieveSome<T>(ids));

        /// <summary>
        /// Get all of ArkhEntities of a specified type
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <returns>A complete collection of all the items being stored</returns>
        public List<T> RetrieveAll<T>()
            where T : ArkhEntity => GetList<T>()
                .ToList();

        public async Task<List<T>> RetrieveAllAsync<T>()
            where T : ArkhEntity => await Task.Run(() => RetrieveAll<T>());

        /// <summary>
        /// Gets all the items that contain the specified string
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="name">The string of characters being searched for</param>
        /// <returns>A list that contains the data of any of the items found, if any</returns>
        public List<T> RetrieveByName<T>(string name)
            where T : NamedArkhEntity => GetList<T>()
                .Where(item => item.GetName()
                    .Contains(name))
                .ToList();

        public async Task<List<T>> RetrieveByNameAsync<T>(string name)
            where T : NamedArkhEntity => await Task.Run(() => RetrieveByName<T>(name));

        /// <summary>
        /// Update an ArkhEntity having the specified Guid with the data entered, if it exists
        ///     If it doesn't exist, nothing happens
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        /// <param name="data">The data being passed in</param>
        public void Update<T>(Guid id, IData data) 
            where T : ArkhEntity {
            var list = GetList<T>();
            if (list.Any()) {
                var targetItem = list.FirstOrDefault(item => item.Id == id);
                targetItem?.SetData(data);
            }
        }

        public async Task UpdateAsync<T>(Guid id, IData data)
            where T : ArkhEntity => await Task.Run(() => Update<T>(id, data));

        /// <summary>
        /// Delete an ArkhEntity from the collection having the specified Guid id
        ///     If it is not found, nothing happens
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        public void Delete<T>(Guid id) 
            where T : ArkhEntity {
            var list = GetList<T>();
            if (list.Any()) {
                var targetItem = list.FirstOrDefault(item => item.Id == id);
                list.Remove(targetItem);
            }
        }

        public async Task DeleteAsync<T>(Guid id)
            where T : ArkhEntity => await Task.Run(() => Delete<T>(id));
    }
}
