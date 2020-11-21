using System;
using System.Collections.Generic;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;
using ArkhenManufacturing.Library.Repository.InternalRepository;

namespace ArkhenManufacturing.Library
{
    /// <summary>
    /// This acts as a way of interfacing with an IRepository of ArkhEntities
    /// </summary>
    public class Archivist
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Construct an Archivist with a specified IRepository
        /// </summary>
        /// <param name="repository">The IRepository instance that the class will be calling methods on</param>
        public Archivist(IRepository repository) {
            _repository = repository ?? new InternalRepository();
        }

        #region Utility Operations

        /// <summary>
        /// Method that checks for any ArkhEntities within the collection
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <returns>True if any are present, otherwise returns false</returns>
        public bool AnyEntities<T>()
            where T : ArkhEntity => _repository.Any<T>();

        /// <summary>
        /// Checks for an ArkhEntity with the specified Guid
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        /// <returns>An ArkhEntity, if any was found. If it wasn't found, return null</returns>
        public bool EntityExists<T>(Guid id)
            where T : ArkhEntity => _repository.Exists<T>(id);

        /// <summary>
        /// Gets all the items that contain the specified string
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="name">The string of characters being searched for</param>
        /// <returns>A list that contains the data of any of the items found, if any</returns>
        public List<IData> GetEntitiesByName<T>(string name)
            where T : NamedArkhEntity => _repository.RetrieveByName<T>(name);

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Create an ArkhEntity child with the specified data
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="data">The specific data type being passed in</param>
        public void CreateEntity<T>(IData data)
            where T : ArkhEntity, new() => _repository.Create<T>(data);

        /// <summary>
        /// Get all of ArkhEntities of a specified type
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <returns>A complete collection of all the items being stored</returns>
        public List<IData> RetrieveAllEntities<T>()
            where T : ArkhEntity => _repository.RetrieveAll<T>();

        /// <summary>
        /// Get an item using a Guid id
        ///     If it doesn't exist, it returns null
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the item</param>
        /// <returns>The item that has the specified Guid, otherwise it returns null</returns>
        public IData RetrieveEntity<T>(Guid id)
            where T : ArkhEntity => _repository.Retrieve<T>(id);

        /// <summary>
        /// Update an ArkhEntity having the specified Guid with the data entered, if it exists
        ///     If it doesn't exist, nothing happens
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        /// <param name="data">The data being passed in</param>
        public void UpdateEntity<T>(Guid id, IData data)
            where T : ArkhEntity => _repository.Update<T>(id, data);

        /// <summary>
        /// Delete an ArkhEntity from the collection having the specified Guid id
        ///     If it is not found, nothing happens
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        public void DeleteEntity<T>(Guid id)
            where T : ArkhEntity => _repository.Delete<T>(id);

        #endregion
    }
}