using System;

namespace ArkhenManufacturing.Library.Data
{
    public abstract class NamedData : IData
    {
        public string Name { get; set; }

        public NamedData(string name) {
            Name = name;
        }
    }
}
