namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Container for the actual data of an Product
    ///     (except for its name)
    /// </summary>
    public class ProductData : NamedData
    {
        /// <summary>
        /// Constructor that allows the Product's name to be set
        /// </summary>
        /// <param name="name"></param>
        public ProductData(string name) :
            base(name) {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">The other instance with the data being assigned to this</param>
        public ProductData(ProductData other) :
            this(other.Name) {
        }
    }
}
