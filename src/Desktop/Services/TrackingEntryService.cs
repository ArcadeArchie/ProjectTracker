using System;
using System.Collections.Generic;
using System.Linq;
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

        public TrackingEntry Find(params object[] keys)
        {
            return _context.TrackingEntries.Find(keys);
        }

        public IEnumerable<TrackingEntry> LoadEntries(string projectname)
        {
            return _context.TrackingEntries.Where(x => x.ProjectName == projectname);
        }

        public EntityEntry<TrackingEntry> SaveEntry(TrackingEntry entry)
        {
            entry.Id = Guid.NewGuid();
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