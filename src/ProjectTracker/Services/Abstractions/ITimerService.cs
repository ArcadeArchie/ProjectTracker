using System;
using System.Timers;

namespace ProjectTracker.Services.Abstractions
{
    public interface ITimerService
    {
        Timer Timer { get; }

        event EventHandler TimerTick;

        DateTime Start();
        DateTime Stop();
    }
}