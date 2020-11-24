using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;

namespace ArkhenManufacturing.Library.Repository.InternalRepository
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

        /// <summary>
        /// Gets the count in a collection
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity in the collection</typeparam>
        /// <returns>An unsigned int of the number in the collection</returns>
        public int Count<T>()
            where T : ArkhEntity => GetList<T>().Count;

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

        /// <summary>
        /// Get all of ArkhEntities of a specified type
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <returns>A complete collection of all the items being stored</returns>
        public List<T> RetrieveAll<T>()
            where T : ArkhEntity => GetList<T>()
                .ToList();

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
                var item = list.FirstOrDefault(item => item.Id == id);
                item.SetData(data);
            }
        }

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
                var item = list.FirstOrDefault(item => item.Id == id);
                list.Remove(item);
            }
        }
    }
}
