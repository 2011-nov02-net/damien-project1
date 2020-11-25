using System;
using ArkhenManufacturing.Library.Data;

namespace ArkhenManufacturing.Library.Entity
{
    /// <summary>
    /// The base entity that is used for the operations in this library;
    ///     it is the penultimate class, other than object, that precedes this
    /// </summary>
    public abstract class ArkhEntity
    {
        /// <summary>
        /// An id that uniquely identifies a subclass instance of
        ///     this class
        /// </summary>
        public Guid Id { get; internal set; }

        /*
            /// <summary>
            /// Constructor that is used in the creation 
            ///     generalizing 
            /// </summary>
            protected ArkhEntity() { }
         */

        /// <summary>
        /// Constructor that takes in a Guid id and assigns it
        /// </summary>
        /// <param name="id">Guid id being assigned to this instance</param>
        internal ArkhEntity(Guid id) => Id = id;

        /// <summary>
        /// Method declaration that allows accessing the data from an ArkhEntity subclass
        /// </summary>
        /// <returns>An IData instance containing the data of the subclass instance</returns>
        public abstract IData GetData();

        /// <summary>
        /// Method declaration allowing for the data to be mutated to the state passed in
        /// </summary>
        /// <param name="data">The data that is being used for the mutation</param>
        public abstract void SetData(IData data);
    }
}