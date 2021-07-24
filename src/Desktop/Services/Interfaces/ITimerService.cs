using Avalonia.Threading;
using System;

namespace ProjectTracker.Desktop.Services.Interfaces
{
    public interface ITimerService
    {
        DispatcherTimer Timer { get; }

        event EventHandler TimerTick;

        DateTime Start();
        DateTime Stop();
    }
}