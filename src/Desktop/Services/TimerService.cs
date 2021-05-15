﻿using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracker.Services
{
    public class TimerService : ITimerService
    {
        public DispatcherTimer Timer { get; private set; }

        public event EventHandler TimerTick;

        public TimerService()
        {
            Timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
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
