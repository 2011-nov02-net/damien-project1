using System;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Extensions;

namespace ArkhenManufacturing.Library.Entity
{
    /// <summary>
    /// An artifice that has the name 'Product', but only relates a Guid id 
    ///     to it and holds the data object that its actual data
    ///     will be stored in.
    /// </summary>
    public sealed class Product : NamedArkhEntity
    {
        /// <summary>
        /// The Object where the actual data is being stored
        /// </summary>
        private ProductData Data { get; set; }

        /// <summary>
        /// Default constructor that assigns the guid to a new Guid
        /// </summary>
        public Product() :
            base(Guid.NewGuid()) { 
        }

        /// <summary>
        /// Constructor that allows assigning the id
        ///     and the data object
        /// </summary>
        /// <param name="id">The Guid id being assigned to this</param>
        /// <param name="data">The data being assigned to this</param>
        public Product(Guid id, ProductData data) :
            base(id) {
            SetData(data);
        }

        /// <summary>
        /// Accessor for the data
        /// </summary>
        /// <returns>Returns the ProductData stored in this class as an IData</returns>
        public override IData GetData() => Data;

        /// <summary>
        /// Mutator for the data
        /// </summary>
        /// <param name="data">The data that is being assigned in this class</param>
        public override void SetData(IData data) {
            data.NullCheck(nameof(data));
            
            Data = data switch
            {
                ProductData productData => productData,
                _ => throw new ArgumentException($"Got '{data.GetType().Name}' instead of '{Data.GetType().Name}'")
            };
        }

        /// <summary>
        /// Method definition that exposes this subclass's name
        /// </summary>
        /// <returns>The name of this subclass, as a string</returns>
        public override string GetName() => Data.Name;
    }
}
