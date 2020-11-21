using System;

namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Container for the actual data of an Product
    ///     (except for its name)
    /// </summary>
    public class ProductData : NamedData
    {
        /// <summary>
        /// Default constructor for use with assigning the data in 
        ///     object initialization syntax
        /// </summary>
        public ProductData() :
            base("") { }

        /// <summary>
        /// Constructor that allows the Product's name to be set
        /// </summary>
        /// <param name="name"></param>
        public ProductData(string name) :
            base(name) {
        }
    }
}
