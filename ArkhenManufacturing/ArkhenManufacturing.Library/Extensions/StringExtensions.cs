using System;

namespace ArkhenManufacturing.Library.Extensions
{
    /// <summary>
    /// Static class that holds extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if a string is null or empty by using string.IsNullOrEmpty(string)
        /// </summary>
        /// <param name="str">The string being targeted</param>
        /// <returns>True if the string is null or empty, false if it has a value that isn't an empty string</returns>
        public static bool IsNullOrEmpty(this string str) {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Checks if a string is null. If it is null, an 
        ///     ArgumentException(string) will be thrown
        /// </summary>
        /// <param name="str">The string being targeted</param>
        /// <param name="strName">The name of the string variable</param>
        public static void NullCheck(this string str, string strName) {
            if (str is null) {
                throw new ArgumentException($"{strName} cannot be null.");
            }
        }

        /// <summary>
        /// Checks if a string is null or empty. If it is null or empty, 
        ///     an ArgumentException(string) will be thrown
        /// </summary>
        /// <param name="str">The string being targeted</param>
        /// <param name="strName">The name of the string variable</param>
        public static void NullOrEmptyCheck(this string str, string strName) {
            if (str.IsNullOrEmpty()) {
                throw new ArgumentException($"{strName} cannot be null or empty.");
            }
        }

    }
}
