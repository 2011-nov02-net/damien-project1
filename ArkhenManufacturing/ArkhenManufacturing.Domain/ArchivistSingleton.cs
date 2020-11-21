using System;
using System.Collections.Generic;

using ArkhenManufacturing.Domain.Database;
using ArkhenManufacturing.Library;
using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;

namespace ArkhenManufacturing.Domain
{
    /// <summary>
    /// This partial class houses the singleton and provides the static functionality of 
    ///     the class
    /// </summary>
    public class ArchivistInterface
    {
        private static Archivist s_Archivist;

        /// <summary>
        /// Creates a new instance of an Archivist if it doesn't already exist.
        ///     This overload creates an Archivist with its default configuration, which
        ///     uses its own internal IRepository
        /// </summary>
        public static void Initialize() {
            Initialize(repository: null);
        }

        /// <summary>
        /// Creates a new instance of an Archivist if it doesn't already exist.
        ///     This overload creates a DatabaseRepository (since it presumes the calling code is passing in a
        ///     connection string
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString) {
            var repository = new DatabaseRepository(connectionString);
            Initialize(repository);
        }

        /// <summary>
        /// Creates a new instance of an Archivist if it doesn't already exist.
        ///     This overload creates an Archivist with the specified IRepository
        ///     If it is null, the Archivist will resort to its own internal IRepository
        /// </summary>
        /// <param name="repository">The repository that the calling code creates</param>
        public static void Initialize(IRepository repository) {
            s_Archivist ??= new Archivist(repository);
        }

        #region Utility Operations

        /// <summary>
        /// Checks if any items are present in the Items Collection
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <returns>True if any are found, elsewise it returns false</returns>
        public static bool Any<T>()
            where T : ArkhEntity => s_Archivist.AnyEntities<T>();

        /// <summary>
        /// Searches for a specified id inside a certain factory
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        /// <returns>True if it was false, otherwise it returns false</returns>
        public static bool Exists<T>(Guid id)
            where T : ArkhEntity => s_Archivist.EntityExists<T>(id);

        /// <summary>
        /// Gets all the items that contain the specified string
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="name">The string of characters being searched for</param>
        /// <returns>A list that contains the data of any of the items found, if any</returns>
        public static List<IData> GetByName<T>(string name)
            where T : NamedArkhEntity => s_Archivist.GetEntitiesByName<T>(name);

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Create an ArkhEntity child with the specified data
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="data">The specific data type being passed in</param>
        public static void Create<T>(IData data)
            where T : ArkhEntity, new() => s_Archivist.CreateEntity<T>(data);

        /// <summary>
        /// Get all of ArkhEntities of a specified type
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <returns>A complete collection of all the items being stored</returns>
        public static List<IData> RetrieveAll<T>()
            where T : ArkhEntity => s_Archivist.RetrieveAllEntities<T>();

        /// <summary>
        /// Get an item using a Guid id
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the item</param>
        /// <returns>The item that has the specified Guid, otherwise it returns null</returns>
        public static IData Retrieve<T>(Guid id)
            where T : ArkhEntity => s_Archivist.RetrieveEntity<T>(id);

        /// <summary>
        /// Update an ArkhEntity having the specified Guid with the data entered, if it exists
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        /// <param name="data">The data being passed in</param>
        public static void Update<T>(Guid id, IData data)
            where T : ArkhEntity => s_Archivist.UpdateEntity<T>(id, data);

        /// <summary>
        /// Delete an ArkhEntity from the collection having the specified Guid id
        ///     If it is not found, nothing happens
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        public static void Delete<T>(Guid id)
            where T : ArkhEntity => s_Archivist.DeleteEntity<T>(id);

        #endregion
    }
}
