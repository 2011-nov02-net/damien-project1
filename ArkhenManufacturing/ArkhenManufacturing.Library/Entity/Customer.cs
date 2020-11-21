using System;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Extensions;

namespace ArkhenManufacturing.Library.Entity
{
    /// <summary>
    /// An arteface that has the name 'Customer', but only relates a Guid id 
    ///     to it and holds the data object that its actual data
    ///     will be stored in.
    /// </summary>
    public sealed class Customer : NamedArkhEntity
    {
        /// <summary>
        /// The Object where the actual data is being stored
        /// </summary>
        internal CustomerData Data { get; set; }

        /// <summary>
        /// Default constructor that assigns the guid to a new Guid
        /// </summary>
        public Customer() :
            base(Guid.NewGuid()) {
        }

        /// <summary>
        /// Accessor for the data
        /// </summary>
        /// <returns>Returns the CustomerData stored in this class as an IData</returns>
        public override IData GetData() => Data;

        /// <summary>
        /// Mutator for the data
        /// </summary>
        /// <param name="data">The data that is being assigned in this class</param>
        public override void SetData(IData data) {
            data.NullCheck(nameof(data));

            Data = data switch
            {
                CustomerData customerData => customerData,
                _ => throw new ArgumentException($"Got '{data.GetType().Name}' instead of '{Data.GetType().Name}'")
            };
        }

        /// <summary>
        /// Method definition that exposes this subclass's name
        /// </summary>
        /// <returns>The name of this subclass, as a string</returns>
        internal override string GetName() => Data.Fullname;
    }
}
