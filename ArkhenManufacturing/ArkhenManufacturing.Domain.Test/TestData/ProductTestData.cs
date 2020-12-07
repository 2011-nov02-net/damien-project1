using System;
using System.Collections.Generic;

namespace ArkhenManufacturing.Domain.Test.TestData
{
    public static class ProductTestData
    {
        public static class Create
        {
            /* Product:
             *  Name
             */
            public static IEnumerable<object> Valid
            {
                get
                {
                    return new List<object[]>
                    {
                        new object[] { "Whole Bag of Coffee Beans" },
                        new object[] { "Garlic cloves" },
                        new object[] { "Ankh" }
                    };
                }
            }

            public static IEnumerable<object> Invalid
            {
                get
                {
                    return new List<object[]>
                    {
                        new object[] { null },
                        new object[] { "" }
                    };
                }
            }
        }

        public static class Retrieve
        {
            public static IEnumerable<object> Valid
            {
                get
                {
                    return null;
                }
            }

            public static IEnumerable<object> Invalid
            {
                get
                {
                    return null;
                }
            }
        }

        public static class Update
        {
            public static IEnumerable<object> Valid
            {
                get
                {
                    return null;
                }
            }

            public static IEnumerable<object> Invalid
            {
                get
                {
                    return null;
                }
            }
        }

        public static class Delete
        {
            public static IEnumerable<object> Valid
            {
                get
                {
                    return null;
                }
            }

            public static IEnumerable<object> Invalid
            {
                get
                {
                    return null;
                }
            }
        }
    }
}
