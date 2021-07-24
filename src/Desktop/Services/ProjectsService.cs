using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectTracker.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Desktop.Services.Interfaces;
using ProjectTracker.Desktop.Data;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Project> LoadEntries(string key)
        {
            throw new NotImplementedException();
        }

        public EntityEntry<Project> SaveEntry(Project entry)
        {
            throw new NotImplementedException();
        }

        public ValueTask<EntityEntry<Project>> SaveEntryAsync(Project entry)
        {
            throw new NotImplementedException();
        }
    }
}
