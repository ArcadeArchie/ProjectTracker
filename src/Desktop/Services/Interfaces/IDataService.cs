using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectTracker.Models;

namespace Desktop.Services
{
    public interface IDataService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> LoadEntries();
        TEntity Find();
        EntityEntry<TEntity> SaveEntry(TEntity entry);
        ValueTask<EntityEntry<TEntity>> SaveEntryAsync(TEntity entry);
    }
}