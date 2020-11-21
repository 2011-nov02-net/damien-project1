using System;

using ArkhenManufacturing.Library.Data;

namespace ArkhenManufacturing.Library.Entity
{
    /// <summary>
    /// An arteface that has the name 'Customer', but only relates a Guid id 
    ///     to it and holds the data object that its actual data
    ///     will be stored in.
    /// </summary>
    public abstract class User : NamedArkhEntity
    {
        private UserData _data;

        /// <summary>
        /// The Object where the actual data is being stored
        /// </summary>
        internal UserData Data
        {
            get => _data;
            set => _data = value ?? throw new ArgumentException("The data for this class cannot be null.");
        }

        /// <summary>
        /// Constructor that allows assigning the id
        ///     and the data object
        /// </summary>
        /// <param name="id">The Guid id being assigned to this</param>
        /// <param name="data">The data being assigned to this</param>
        public User(Guid id, UserData data) :
            base(id) {
            Data = data;
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
            Data = data switch
            {
                UserData userData => userData,
                _ => throw new ArgumentException($"Got '{nameof(data)}' instead of '{nameof(Data)}'")
            };
        }

        /// <summary>
        /// Method definition that exposes this subclass's name
        /// </summary>
        /// <returns>The name of this subclass, as a string</returns>
        internal override string GetName() => Data.Fullname;
    }
}
