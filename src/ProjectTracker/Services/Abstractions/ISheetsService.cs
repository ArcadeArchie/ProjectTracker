using ProjectTracker.Models;
using System.Collections.Generic;

namespace ProjectTracker.Services.Abstractions
{
    public interface ISheetsService
    {
        void Export(List<IList<object>> saves, int? offset = null);
        IEnumerable<TrackingEntry> LoadTrackingEntries();

    }
}