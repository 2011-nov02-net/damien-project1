using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ArkhenManufacturing.Library.Data;
using ArkhenManufacturing.Library.Entity;

namespace ArkhenManufacturing.Domain.Database.DbSetInterfacer
{
    internal interface IInterfacer { }

    internal interface IDbSetInterfacer<T> : IInterfacer
        where T : ArkhEntity
    {
        Task<bool> AnyAsync();
        Task<bool> ExistsAsync(Guid id);
        Task<int> CountAsync();
        Task<Guid> CreateAsync(IData data);
        Task<T> RetrieveAsync(Guid id);
        Task<ICollection<T>> RetrieveSomeAsync(ICollection<Guid> ids);
        Task<ICollection<T>> RetrieveAllAsync();
        Task UpdateAsync(Guid id, IData data);
        Task DeleteAsync(Guid id);
    }
}
