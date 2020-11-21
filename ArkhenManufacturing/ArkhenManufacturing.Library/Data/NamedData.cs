namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Abstract class that is a subset of IData
    ///     and denotes that the item has a name
    ///     that is accessible
    /// </summary>
    public abstract class NamedData : IData
    {
        /// <summary>
        /// The name of the data
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor to set the name
        /// </summary>
        /// <param name="name">The name of the data</param>
        public NamedData(string name) {
            Name = name;
        }
    }
}
