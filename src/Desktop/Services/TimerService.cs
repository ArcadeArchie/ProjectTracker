using ProjectTracker.Services.Abstractions;
using System;
using System.Timers;

namespace ProjectTracker.Desktop.Services
{
    public class TimerService : ITimerService
    {
        public Timer Timer { get; private set; }

        public event EventHandler TimerTick;
        public bool Enabled { get => Timer.Enabled; }
        public TimerService()
        {
            Timer = new Timer
            {
                Interval = 1000
            };
            Timer.Elapsed += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            TimerTick?.Invoke(sender, e);
        }

        public DateTime Start()
        {
            Timer.Start();
            return DateTime.Now;
        }
        public DateTime Stop()
        {
            Timer.Stop();
            return DateTime.Now;
        }
    }
}
