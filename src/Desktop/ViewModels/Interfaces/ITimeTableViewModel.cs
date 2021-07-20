using System.Collections.ObjectModel;
using System.Reactive;
using ProjectTracker.Models;
using ReactiveUI;

namespace Desktop.ViewModels.Interfaces
{
    public interface ITimeTableViewModel
    {
        string StartBtnText { get; set; }
        string TimerText { get; set; }
        bool IsRunning { get; set; }
        ObservableCollection<TrackingEntry> Items { get; set; }
        ReactiveCommand<Unit, Unit> StartTimerCmd { get; }
    }
}