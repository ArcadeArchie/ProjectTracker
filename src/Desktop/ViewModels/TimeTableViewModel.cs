using Avalonia.Controls;
using Desktop.Data;
using Desktop.Services;
using Desktop.ViewModels.Interfaces;
using DynamicData;
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
    public class TimeTableViewModel : ListViewModelBase<TrackingEntry>, ITimeTableViewModel
    {
        private readonly Timer _timer;

        #region Properties
        protected override TrackingEntryService _dataService { get; }
        
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

        public override ObservableCollection<TrackingEntry> Items { get; set; } = new ObservableCollection<TrackingEntry>();

        public ReactiveCommand<Unit, Unit> StartTimerCmd { get; }

        public ReactiveCommand<System.Collections.IList, Unit> DeleteRowCmd { get; }
        public ReactiveCommand<Unit, Unit> LoadRowsCmd { get; }
        public ReactiveCommand<System.Collections.IList, Unit> SaveRowsCmd { get; }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Only there so the so the Designer works properly</remarks>
        public TimeTableViewModel(): base(null) { }
        public TimeTableViewModel(IDataService<TrackingEntry> dataService): base(dataService)
        {
            StartBtnText = "Start Timer";
            _timer = new Timer(1000);
            _timer.Elapsed += timer_Elapsed;
            StartTimerCmd = ReactiveCommand.Create(HandleTimerCmd, this.ObservableForProperty(x => x.ProjectName).Select(_ => !String.IsNullOrWhiteSpace(ProjectName)));
            DeleteRowCmd = ReactiveCommand.Create<System.Collections.IList>(HandleDeleteRow);
            LoadRowsCmd = ReactiveCommand.Create(HandleLoadRows, this.ObservableForProperty(x => x.ProjectName).Select(_ => !String.IsNullOrWhiteSpace(ProjectName)));
            SaveRowsCmd = ReactiveCommand.Create<System.Collections.IList>(HandleSaveRows, this.ObservableForProperty(x => x.Items.Count).Select(_ => Items.Any()));
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

        #region HandleSaveRows

        void HandleSaveRows(System.Collections.IList items)
        {
            foreach (TrackingEntry item in items)
            {   
                if(item.Id == Guid.Empty)
                    _dataService?.SaveEntry(item);
            }
        }

        #endregion

        #region HandleLoadRows

        void HandleLoadRows()
        {
            if (_dataService == null)
                return;
            Items.RemoveMany(Items.Where(x => x.Id != Guid.Empty));
            Items.AddRange(_dataService.LoadEntries(ProjectName));
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
