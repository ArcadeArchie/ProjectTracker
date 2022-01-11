using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProjectTracker.Services.Abstractions
{
    public interface IDataService<TEntity> where TEntity : class
    {
        ValueTask<IEnumerable<TEntity>> LoadEntriesAsync();
        IEnumerable<TEntity> LoadEntries();
        IEnumerable<TEntity> LoadEntries(string key);
        TEntity Find(params object[] keys);
        EntityEntry<TEntity> SaveEntry(TEntity entry);
        ValueTask<EntityEntry<TEntity>> SaveEntryAsync(TEntity entry);
        void DeleteEntry(TEntity entry);
        Task DeleteEntryAsync(TEntity entry);
    }
}