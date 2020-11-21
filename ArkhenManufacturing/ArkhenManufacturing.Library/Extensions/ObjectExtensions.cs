using System;

namespace ArkhenManufacturing.Library.Extensions
{
    /// <summary>
    /// Static class that holds extension methods for objects
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks if the object is null
        /// </summary>
        /// <param name="obj">The object being targeted</param>
        /// <param name="objName">The name of the object being passed in</param>
        public static void NullCheck(this object obj, string objName) {
            if(obj is null) {
                throw new ArgumentException($"{objName} cannot be null.");
            }
        }
    }
}
