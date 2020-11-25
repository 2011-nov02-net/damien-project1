using System;

namespace ArkhenManufacturing.Library.Entity
{
    /// <summary>
    /// Abstract class that denotes an ArkhEntity
    ///     that has a name exposed
    /// </summary>
    public abstract class NamedArkhEntity : ArkhEntity
    {
        /// <summary>
        /// Constructor that assigns a Guid id to this instance
        /// </summary>
        /// <param name="id">The Guid id being assigned</param>
        internal NamedArkhEntity(Guid id) : 
            base(id) { 
        }

        /// <summary>
        /// Method declaration that states that a subclass will either
        ///     have a definition for name retrieval,
        ///     or it will be abstract and pass it on
        /// </summary>
        /// <returns>The name of the subclass, as a string</returns>
        public abstract string GetName();
    }
}
