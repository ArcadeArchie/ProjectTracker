using Avalonia.Threading;
using System;

namespace ProjectTracker.Services
{
    public interface ITimerService
    {
        DispatcherTimer Timer { get; }

        event EventHandler TimerTick;

        DateTime Start();
        DateTime Stop();
    }
}