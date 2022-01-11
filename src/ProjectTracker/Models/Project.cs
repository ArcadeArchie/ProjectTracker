using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<TrackingEntry> TrackingEntries { get; set; }
    }
}
