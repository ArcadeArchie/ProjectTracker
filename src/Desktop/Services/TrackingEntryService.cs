using System.Collections.Generic;
using System.Threading.Tasks;
using Desktop.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectTracker.Models;

namespace Desktop.Services
{
    public class TrackingEntryService : IDataService<TrackingEntry>
    {
        private readonly AppDbContext _context;
        public TrackingEntryService(AppDbContext context)
        {
            _context = context; 
        }

        public TrackingEntry Find()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TrackingEntry> LoadEntries()
        {
            throw new System.NotImplementedException();
        }

        public EntityEntry<TrackingEntry> SaveEntry(TrackingEntry entry)
        {
            var dbEntry = _context.TrackingEntries.Add(entry);
            _context.SaveChanges();
            return dbEntry;
        }

        public ValueTask<EntityEntry<TrackingEntry>> SaveEntryAsync(TrackingEntry entry)
        {
            throw new System.NotImplementedException();
        }
    }
}