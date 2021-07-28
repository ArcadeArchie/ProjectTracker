using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectTracker.Desktop.Data;
using ProjectTracker.Desktop.Models;
using ProjectTracker.Desktop.Services.Interfaces;

namespace ProjectTracker.Desktop.Services
{
    public class TrackingEntryService : IDataService<TrackingEntry>
    {
        private readonly AppDbContext _context;
        public TrackingEntryService(AppDbContext context)
        {
            _context = context; 
        }

        public void DeleteEntry(TrackingEntry entry)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntryAsync(TrackingEntry entry)
        {
            throw new NotImplementedException();
        }

        public TrackingEntry Find(params object[] keys)
        {
            return _context.TrackingEntries.Find(keys);
        }

        public IEnumerable<TrackingEntry> LoadEntries(string projectname)
        {
            var entries = _context.TrackingEntries.Where(x => x.Project.Name == projectname);
            if (entries.Any())
            {
                return entries;
            }
            return new List<TrackingEntry>();
        }

        public IEnumerable<TrackingEntry> LoadEntries()
        {
            throw new NotImplementedException();
        }

        public ValueTask<IEnumerable<TrackingEntry>> LoadEntriesAsync()
        {
            throw new NotImplementedException();
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