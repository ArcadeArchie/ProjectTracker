using ProjectTracker.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ProjectTracker.ViewModels
{
    public class TimeTableViewModel : ViewModelBase
    {
        private readonly Timer _timer;

        #region Properties

        #region StartBtnText
        private string _startBtnText;
        public string StartBtnText
        {
            get => _startBtnText;
            set => this.RaiseAndSetIfChanged(ref _startBtnText, value);
        }
        #endregion
        
        #region TimerText
        private string _timerText;
        public string TimerText
        {
            get => _timerText;
            set => this.RaiseAndSetIfChanged(ref _timerText, value);
        }
        #endregion

        #region IsRunning

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => this.RaiseAndSetIfChanged(ref _isRunning, value);
        }

        #endregion

        public ObservableCollection<TrackingEntry> Items { get; set; } = new ObservableCollection<TrackingEntry>();

        public ReactiveCommand<Unit, Unit> StartTimerCmd { get; }
        public ReactiveCommand<Unit, Unit> RestartTimerCmd { get; }


        #endregion


        public TimeTableViewModel()
        {
            StartBtnText = "Start Timer";
            _timer = new Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
            StartTimerCmd = ReactiveCommand.Create(HandleTimerCmd);
            RestartTimerCmd = ReactiveCommand.Create(HandleResetCmd, this.WhenAnyValue(x => x.IsRunning));
        }
        private int elapsedTime;
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            elapsedTime += 1;
            TimerText = TimeSpan.FromSeconds(elapsedTime).ToString(@"hh\:mm\:ss");
        }
        void HandleResetCmd()
        {
            TimerText = "00:00:00";
            elapsedTime = 0;
            HandleTimerCmd();
        }
        void HandleTimerCmd()
        {
            if (!_timer.Enabled)
            {
                _timer.Start();
                TimerText = "00:00:00";
                StartBtnText = "Stop Timer";
            }
            else
            {
                _timer.Stop();
                StartBtnText = "Start Timer";
            }
            IsRunning = _timer.Enabled;
        }
    }
}
