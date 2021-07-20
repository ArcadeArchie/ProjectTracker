using Avalonia.Controls;
using Desktop.Data;
using Desktop.Services;
using Desktop.ViewModels.Interfaces;
using ProjectTracker.Models;
using ReactiveUI;
using Splat;
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
    public class TimeTableViewModel : ViewModelBase, ITimeTableViewModel
    {
        private readonly TrackingEntryService _dataService;
        private readonly Timer _timer;

        #region Properties

        #region ProjectName
        private string _projectName;
        public string ProjectName
        {
            get => _projectName;
            set => this.RaiseAndSetIfChanged(ref _projectName, value);
        }
        #endregion

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

        public ReactiveCommand<System.Collections.IList, Unit> DeleteRowCmd { get; }
        public ReactiveCommand<DataGrid, Unit> LoadRowsCmd { get; }
        public ReactiveCommand<DataGrid, Unit> SaveRowsCmd { get; }

        #endregion

        public TimeTableViewModel() { }
        public TimeTableViewModel(IDataService<TrackingEntry> dataService)
        {
            _dataService = (TrackingEntryService)dataService;
            StartBtnText = "Start Timer";
            _timer = new Timer(1000);
            _timer.Elapsed += timer_Elapsed;
            StartTimerCmd = ReactiveCommand.Create(HandleTimerCmd);
            DeleteRowCmd = ReactiveCommand.Create<System.Collections.IList>(HandleDeleteRow);
        }
        #region Methods

        #region CommandActions

        #region AddEntry
        /// <summary>
        /// 
        /// </summary>
        private void AddEntry()
        {
            var entry = new TrackingEntry
            {
                ProjectName = ProjectName,
                BeenPaid = false,
                TimeStamp = DateTimeOffset.Now,
                Duration = TimeSpan.FromSeconds(elapsedTime)
            };
            Items.Add(entry);
        }
        #endregion

        #region HandleTimerCmd
        /// <summary>
        /// 
        /// </summary>
        void HandleTimerCmd()
        {
            if (!_timer.Enabled)
            {
                _timer.Start();
                StartBtnText = "Stop Timer";
            }
            else
            {
                _timer.Stop();
                StartBtnText = "Start Timer";
                AddEntry();
            }
            IsRunning = _timer.Enabled;
        }
        #endregion

        #region HandleDeleteRow
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        private void HandleDeleteRow(System.Collections.IList selectedItems)
        {
            var itemsToRemove = new List<TrackingEntry>();
            foreach (TrackingEntry item in selectedItems)
            {
                itemsToRemove.Add(item);
            }
            foreach (var item in itemsToRemove)
            {
                Items.Remove(item);
            }
        }
        #endregion

        #endregion



        private int elapsedTime;
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            elapsedTime += 1;
            TimerText = TimeSpan.FromSeconds(elapsedTime).ToString(@"hh\:mm\:ss");
        }
        #endregion


    }
}
