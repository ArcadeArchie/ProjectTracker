using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProjectTracker.Desktop.Services.Interfaces
{
    public interface IDataService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> LoadEntries(string key);
        TEntity Find(params object[] keys);
        EntityEntry<TEntity> SaveEntry(TEntity entry);
        ValueTask<EntityEntry<TEntity>> SaveEntryAsync(TEntity entry);
    }
}