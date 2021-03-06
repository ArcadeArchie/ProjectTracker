using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Desktop.Data;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Services.Abstractions;
using ProjectTracker.Models;

namespace ProjectTracker.Desktop.Services
{
    public class ProjectsService : IDataService<Project>
    {
        private readonly AppDbContext _context;
        public ProjectsService(AppDbContext context)
        {
            _context = context;
        }
        public Project Find(params object[] keys)
        {
            return _context.Projects.Find(keys);
        }

        public async ValueTask<IEnumerable<Project>> LoadEntriesAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public IEnumerable<Project> LoadEntries()
        {
            return _context.Projects;
        }

        public IEnumerable<Project> LoadEntries(string key)
        {
            return _context.Projects.Where(x => x.Name == key);
        }

        public EntityEntry<Project> SaveEntry(Project entry)
        {
            entry.Id = Guid.NewGuid();
            var dbEntry = _context.Projects.Add(entry);
            _context.SaveChanges();
            return dbEntry;
        }

        public ValueTask<EntityEntry<Project>> SaveEntryAsync(Project entry)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntry(Project entry)
        {
            var dbEntry = _context.Projects.Find(entry.Id);
            _context.Projects.Remove(dbEntry);
            _context.SaveChanges();
        }

        public async Task DeleteEntryAsync(Project entry)
        {
            var dbEntry = await _context.Projects.FindAsync(entry.Id);
            _context.Projects.Remove(dbEntry);
            await _context.SaveChangesAsync();
        }
    }
}
