using System;

using ArkhenManufacturing.Library.Data;

namespace ArkhenManufacturing.Library.Entity
{
    /// <summary>
    /// An arteface that has the name 'Address', but only relates a Guid id 
    ///     to it and holds the data object that its actual data
    ///     will be stored in.
    /// </summary>
    public sealed class Address : ArkhEntity
    {
        private AddressData _data;

        /// <summary>
        /// The Object where the actual data is being stored
        /// </summary>
        internal AddressData Data
        {
            get => _data;
            set => _data = value ?? throw new ArgumentException("The data for this class cannot be null.");
        }

        /// <summary>
        /// Default constructor that assigns the guid to a new Guid
        /// </summary>
        public Address() :
            base(Guid.NewGuid()) { }

        /// <summary>
        /// Constructor that allows assigning the id
        ///     and the data object
        /// </summary>
        /// <param name="id">The Guid id being assigned to this</param>
        /// <param name="data">The data being assigned to this</param>
        internal Address(Guid id, AddressData data) :
            base(id) {
            Data = data;
        }

        /// <summary>
        /// Accessor for the data
        /// </summary>
        /// <returns>Returns the AddressData stored in this class as an IData</returns>
        public override IData GetData() => Data;

        /// <summary>
        /// Mutator for the data
        /// </summary>
        /// <param name="data">The data that is being assigned in this class</param>
        public override void SetData(IData data) {
            Data = data switch
            {
                AddressData addressData => addressData,
                _ => throw new ArgumentException($"Got '{nameof(data)}' instead of '{nameof(Data)}'")
            };
        }
    }
}
