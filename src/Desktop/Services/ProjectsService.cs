using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectTracker.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Desktop.Services.Interfaces;
using ProjectTracker.Desktop.Data;
using Microsoft.EntityFrameworkCore;

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
    }
}
