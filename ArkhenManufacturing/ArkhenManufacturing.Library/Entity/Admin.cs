using System;

using ArkhenManufacturing.Library.Data;

namespace ArkhenManufacturing.Library.Entity
{
    /// <summary>
    /// An arteface that has the name 'Admin', but only relates a Guid id 
    ///     to it and holds the data object that its actual data
    ///     will be stored in.
    /// </summary>
    public sealed class Admin : User
    {
        private AdminData _data;

        /// <summary>
        /// The Object where the actual data is being stored
        /// </summary>
        internal new AdminData Data
        {
            get => _data;
            set => _data = value ?? throw new ArgumentException("The data for this class cannot be null.");
        }

        /// <summary>
        /// Default constructor that assigns the guid to a new Guid
        /// </summary>
        public Admin() :
            base(Guid.NewGuid(), new AdminData()) {
            Data = base.Data as AdminData;
        }

        /// <summary>
        /// Constructor that allows assigning the id
        ///     and the data object
        /// </summary>
        /// <param name="id">The Guid id being assigned to this</param>
        /// <param name="data">The data being assigned to this</param>
        public Admin(Guid id, AdminData data) :
            base(id, data) {
            Data = data;
        }

        /// <summary>
        /// Accessor for the data
        /// </summary>
        /// <returns>Returns the AdminData stored in this class as an IData</returns>
        public override IData GetData() => Data;

        /// <summary>
        /// Mutator for the data
        /// </summary>
        /// <param name="data">The data that is being assigned in this class</param>
        public override void SetData(IData data) {
            Data = data switch
            {
                AdminData adminData => adminData,
                _ => throw new ArgumentException($"Got '{nameof(data)}' instead of '{nameof(Data)}'")
            };
        }
    }
}
