using ProjectTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.ViewModels
{
    public class TimeTableViewModel : ViewModelBase
    {
        public ObservableCollection<TrackingEntry> Items { get; set; }

        public TimeTableViewModel()
        {
            Items = new ObservableCollection<TrackingEntry>(new[]
            {
                new TrackingEntry
                {
                    ProjectName = "test",
                    Duration = TimeSpan.MaxValue
                },
                new TrackingEntry
                {
                    ProjectName = "test",
                    Duration = TimeSpan.MaxValue
                },
                new TrackingEntry
                {
                    ProjectName = "test",
                    Duration = TimeSpan.MaxValue
                },
                new TrackingEntry
                {
                    ProjectName = "test",
                    Duration = TimeSpan.MaxValue
                },
                new TrackingEntry
                {
                    ProjectName = "test",
                    Duration = TimeSpan.MaxValue
                },
                new TrackingEntry
                {
                    ProjectName = "test",
                    Duration = TimeSpan.MaxValue
                },
                new TrackingEntry
                {
                    ProjectName = "test",
                    Duration = TimeSpan.MaxValue
                },
                new TrackingEntry
                {
                    ProjectName = "test",
                    Duration = TimeSpan.MaxValue
                },
            });
        }
    }
}
