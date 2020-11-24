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
        private static ILogger s_ArchivistLogger;

        /// <summary>
        /// Creates a new instance of an Archivist if it doesn't already exist.
        ///     This overload creates an Archivist with its default configuration, which
        ///     uses its own internal IRepository
        /// </summary>
        public static void Initialize() {
            Initialize(repository: null, archivistLogger: null);
        }

        /// <summary>
        /// Creates a new instance of an Archivist if it doesn't already exist.
        ///     This overload creates a DatabaseRepository (since it presumes the calling code is passing in a
        ///     connection string
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString, ILogger archivistLogger = null, ILogger efCoreLogger = null) {
            var repository = new DatabaseRepository(connectionString, efCoreLogger);
            Initialize(repository, archivistLogger);
        }

        /// <summary>
        /// Creates a new instance of an Archivist if it doesn't already exist.
        ///     This overload creates an Archivist with the specified IRepository
        ///     If it is null, the Archivist will resort to its own internal IRepository
        /// </summary>
        /// <param name="repository">The repository that the calling code creates</param>
        public static void Initialize(IRepository repository, ILogger archivistLogger) {
            s_Archivist ??= new Archivist(repository);
            s_ArchivistLogger = archivistLogger ?? new DebugLogger();
        }

        #region Utility Operations

        /// <summary>
        /// Private method that attempts to run a function that returns a 'T';
        ///     if any exception happens, it will log it to the console, and rethrow it.
        /// </summary>
        /// <typeparam name="T">The type of the return value of the function</typeparam>
        /// <param name="func">The function to be run</param>
        /// <returns>An instance of 'T' that the function passed in returns</returns>
        public static T Try<T>(Func<T> func) {
            T result;

            try {
                // Attempt to do the action
                result = func();
            } catch (Exception ex) {
                // Handle and log the exception
                s_ArchivistLogger.LogLine(ex.Message);

                // Rethrow for the client application to handle
                throw;
            }

            return result;
        }

        /// <summary>
        /// Private method that attempts to run a function that takes in an item 'S', and returns a 'T';
        ///     if any exception happens, it will log it to the console, and rethrow it.
        /// </summary>
        /// <typeparam name="S">The input parameter of the function</typeparam>
        /// <typeparam name="T">The type of the return value of the function</typeparam>
        /// <param name="func">The function to be run</param>
        /// <param name="item">The item being passed into the function as a parameter</param>
        /// <returns>An instance of 'T' that the function passed in returns</returns>
        public static T Try<S, T>(Func<S, T> func, S item) {
            T result;

            try {
                // Attempt to do the action
                result = func(item);
            } catch (Exception ex) {
                // Handle and log the exception
                s_ArchivistLogger.LogLine(ex.Message);

                // Rethrow for the client application to handle
                throw;
            }

            return result;
        }

        /// <summary>
        /// Private method that attempts to run an action that has a 'T' parameter;
        ///     if any exception happens, it will log it to the console, and rethrow it.
        /// </summary>
        /// <typeparam name="T">The input parameter of the function</typeparam>
        /// <param name="action">The action to run</param>
        /// <param name="item">The item being passed into the action as a parameter</param>
        public static void Try<T>(Action<T> action, T item) {
            try {
                // Attempt to do the action
                action(item);
            } catch (Exception ex) {
                // Handle and log the exception
                s_ArchivistLogger.LogLine(ex.Message);

                // Rethrow for the client application to handle
                throw;
            }
        }

        /// <summary>
        /// Private method that attempts to run an action that has an 'S' and a 'T' parameter;
        ///     if any exception happens, it will log it to the console, and rethrow it.
        /// </summary>
        /// <typeparam name="S">Type of the first parameter</typeparam>
        /// <typeparam name="T">Type of the second parameter</typeparam>
        /// <param name="action">The action to run</param>
        /// <param name="item1">First parameter of the action</param>
        /// <param name="item2">Second parameter of the action</param>
        public static void Try<S, T>(Action<S, T> action, S item1, T item2) {
            try {
                // Attempt to do the action
                action(item1, item2);
            } catch (Exception ex) {
                // Handle and log the exception
                s_ArchivistLogger.LogLine(ex.Message);

                // Rethrow for the client application to handle
                throw;
            }
        }

        /// <summary>
        /// Logs a string using the ILogger.Log(string) method without a newline/carriage-return character
        /// </summary>
        /// <param name="message">The string to log</param>
        public static void Log(string message) {
            s_ArchivistLogger.Log(message);
        }

        /// <summary>
        /// Logs a string using the ILogger.LogLine(string) method with a newline/carriage-return character
        /// </summary>
        /// <param name="message">The string to log</param>
        public static void LogLine(string message) {
            s_ArchivistLogger.LogLine(message);
        }

        /// <summary>
        /// Checks if any items are present in the Items Collection
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <returns>True if any are found, elsewise it returns false</returns>
        public static bool Any<T>()
            where T : ArkhEntity => Try<bool>(s_Archivist.AnyEntities<T>);

        /// <summary>
        /// Searches for a specified id inside a certain factory
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        /// <returns>True if it was false, otherwise it returns false</returns>
        public static bool Exists<T>(Guid id)
            where T : ArkhEntity => Try(s_Archivist.EntityExists<T>, id);

        /// <summary>
        /// Gets all the items that contain the specified string
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="name">The string of characters being searched for</param>
        /// <returns>A list that contains the data of any of the items found, if any</returns>
        public static List<T> RetrieveByName<T>(string name)
            where T : NamedArkhEntity => Try(s_Archivist.GetEntitiesByName<T>, name);

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Create an ArkhEntity child with the specified data
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="data">The specific data type being passed in</param>
        public static Guid Create<T>(IData data)
            where T : ArkhEntity, new() => Try(s_Archivist.CreateEntity<T>, data);

        /// <summary>
        /// Get all of ArkhEntities of a specified type
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <returns>A complete collection of all the items being stored</returns>
        public static List<T> RetrieveAll<T>()
            where T : ArkhEntity => Try(s_Archivist.RetrieveAllEntities<T>);

        /// <summary>
        /// Get an item using a Guid id
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the item</param>
        /// <returns>The item that has the specified Guid, otherwise it returns null</returns>
        public static T Retrieve<T>(Guid id)
            where T : ArkhEntity => Try(s_Archivist.RetrieveEntity<T>, id);

        /// <summary>
        /// Update an ArkhEntity having the specified Guid with the data entered, if it exists
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        /// <param name="data">The data being passed in</param>
        public static void Update<T>(Guid id, IData data)
            where T : ArkhEntity => Try(s_Archivist.UpdateEntity<T>, id, data);

        /// <summary>
        /// Delete an ArkhEntity from the collection having the specified Guid id
        ///     If it is not found, nothing happens
        /// </summary>
        /// <typeparam name="T">The type of ArkhEntity targeted</typeparam>
        /// <param name="id">The Guid id of the ArkhEntity</param>
        public static void Delete<T>(Guid id)
            where T : ArkhEntity => Try(s_Archivist.DeleteEntity<T>, id);

        #endregion
    }
}
