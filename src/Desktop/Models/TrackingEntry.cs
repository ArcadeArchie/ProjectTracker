using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    public class TrackingEntry
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Project name")]
        public string ProjectName { get; set; }

        [Display(Name = "Time")]
        public DateTimeOffset TimeStamp { get; set; }

        [Display(Name = "Duration")]
        public TimeSpan Duration { get; set; }

        [Display(Name = "Paid?")]
        public bool BeenPaid { get; set; }
    }
}
