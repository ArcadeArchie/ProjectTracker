using ProjectTracker.Desktop.Models;
using System.Collections.Generic;

namespace ProjectTracker.Desktop.Services.Interfaces
{
    public interface ISheetsService
    {
        void Export(List<IList<object>> saves, int? offset = null);
        IEnumerable<TrackingEntry> LoadTrackingEntries();

    }
}