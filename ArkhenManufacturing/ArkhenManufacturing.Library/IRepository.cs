using System;
using System.Collections.Generic;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;

namespace ArkhenManufacturing.Library
{
    /// <summary>
    /// An interface that declares a contract towards a repository pattern;
    ///     items of the type it is created with can have CRUD operations run on them
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Checks if any items are present in the Items collection
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity in the collection</typeparam>
        /// <returns>True if any are found, false if none are found</returns>
        bool Any<T>() where T : ArkhEntity;

        /// <summary>
        /// Checks if the specified Guid id exists for any of the items
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity in the collection</typeparam>
        /// <param name="id">The Guid id of the item</param>
        /// <returns>True if the item exists in the collection, false if it isn't present</returns>
        bool Exists<T>(Guid id) where T : ArkhEntity;

        /// <summary>
        /// Gets the count in a collection
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity in the collection</typeparam>
        /// <returns>An unsigned int of the number in the collection</returns>
        int Count<T>() where T : ArkhEntity;

        /// <summary>
        /// Get an amount of items that contain a specified name
        /// </summary>
        /// <typeparam name="T">The type of NamedArkhEntity being searched in the collection</typeparam>
        /// <param name="name">A string that can be any portion of the NamedArkhEntity, even the whole string</param>
        /// <returns>A collection of the data of the items that were found with the </returns>
        List<IData> RetrieveByName<T>(string name) where T : NamedArkhEntity;

        /// <summary>
        /// Creates an ArkhEntity of the specified type with the specified data
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity in the collection</typeparam>
        /// <param name="data">The set of data for the item that's being created</param>
        void Create<T>(IData data) where T : ArkhEntity, new();

        /// <summary>
        /// Get all of the items in the collection
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity in the collection</typeparam>
        /// <returns>A List of the data that is found in the collection</returns>
        List<IData> RetrieveAll<T>() where T : ArkhEntity;

        /// <summary>
        /// Gets the data that is paired with the Guid id being passed in.
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity in the collection</typeparam>
        /// <param name="id">The Guid id of the item being searched for</param>
        /// <returns>The specified type of data found if the id exists within the collection. Elsewise, it returns null</returns>
        IData Retrieve<T>(Guid id) where T : ArkhEntity;

        /// <summary>
        /// Updates the ArkhEntity of a specified type
        /// </summary>
        /// <typeparam name="T">The specified type of ArkhEntity</typeparam>
        /// <param name="id">The Guid id of the item</param>
        /// <param name="data">The data that is going to be assigned into the ArkhEntity</param>
        void Update<T>(Guid id, IData data) where T : ArkhEntity;

        /// <summary>
        /// If found, will remove the item with the specified Guid id
        /// </summary>
        /// <typeparam name="T">The specified type of ArkhEntity</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        void Delete<T>(Guid id) where T : ArkhEntity;
    }
}
