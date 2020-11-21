namespace ArkhenManufacturing.Library.Data
{
    /// <summary>
    /// Container for the actual data of an Address
    /// </summary>
    public class AddressData : IData
    {
        /// <summary>
        /// First line of an address
        /// </summary>
        public string Line1 { get; set; }

        /// <summary>
        /// Second line of an address
        /// </summary>
        public string Line2 { get; set; }

        /// <summary>
        /// The city in which this address is located
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The state in which the address is located (if it is in a state)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The country in which the address is located
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The Zip Code of the address
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Default constructor for use with assigning the data in 
        ///     object initialization syntax
        /// </summary>
        public AddressData() { }

        /// <summary>
        /// Constructor that assigns all of the variables in this object
        /// </summary>
        /// <param name="line1">First line of an address</param>
        /// <param name="line2">Second line of an address</param>
        /// <param name="city">The city in which this address is located</param>
        /// <param name="state">The state in which the address is located</param>
        /// <param name="country">The country in which the address is located</param>
        /// <param name="zipCode">The Zip Code of the address</param>
        public AddressData(string line1, string line2, string city, string state, string country, string zipCode) {
            Line1 = line1;
            Line2 = line2;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        /// <summary>
        /// ToString override that formats the data into a string
        /// </summary>
        /// <returns>The data of this object compilated into a string</returns>
        public override string ToString() => $"{Line1},{(!string.IsNullOrWhiteSpace(Line2) ? $" {Line2}, " : "")}{City},{(!string.IsNullOrWhiteSpace(State) ? $" {State}, " : "")}{Country} {ZipCode}";
    }
}
