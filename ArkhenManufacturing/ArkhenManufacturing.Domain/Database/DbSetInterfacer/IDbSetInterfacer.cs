using System;
using System.Collections.Generic;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;

namespace ArkhenManufacturing.Domain.Database.DbSetInterfacer
{
    internal interface IInterfacer { }

    internal interface IDbSetInterfacer<T> : IInterfacer
        where T : ArkhEntity
    {
        bool Any();
        bool Exists(Guid id);
        int Count();
        void Create(IData data);
        ICollection<T> RetrieveAll();
        T Retrieve(Guid id);
        void Update(Guid id, IData data);
        void Delete(Guid id);
    }
}
