using DynamicData;
using ProjectTracker.Desktop.Models;
using ProjectTracker.Desktop.Services;
using ProjectTracker.Desktop.Services.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Timers;

namespace ProjectTracker.Desktop.ViewModels
{
    public class TimeTableViewModel : ListViewModelBase<TrackingEntry>
    {
        private readonly TimerService _timerService;

        #region Properties
        /// <summary>
        /// Current ProjectName
        /// </summary>
        [Reactive]
        public string ProjectName { get; set; }

        /// <summary>
        /// Displaytext for the start button
        /// </summary>
        [Reactive]
        public string StartBtnText { get; set; }

        /// <summary>
        /// Displaytext for the Timer
        /// </summary>
        [Reactive]
        public string TimerText { get; set; }

        /// <summary>
        /// Is the Timmer running?
        /// </summary>
        [Reactive]
        public bool IsRunning { get; set; }

        /// <summary>
        /// Current TrackingEntries
        /// </summary>
        public override ObservableCollection<TrackingEntry> Items { get; set; } = new ObservableCollection<TrackingEntry>();

        /// <summary>
        /// Command for the StartTimer button
        /// </summary>
        public ReactiveCommand<Unit, Unit> StartTimerCmd { get; }

        /// <summary>
        /// Command for the DeleteRows button
        /// </summary>
        public ReactiveCommand<System.Collections.IList, Unit> DeleteRowCmd { get; }

        /// <summary>
        /// Command for the LoadRows button
        /// </summary>
        public ReactiveCommand<Unit, Unit> LoadRowsCmd { get; }

        /// <summary>
        /// Command for the SaveRows button
        /// </summary>
        public ReactiveCommand<System.Collections.IList, Unit> SaveRowsCmd { get; }

        #endregion

        #region Constructors

        /// <summary></summary>
        /// <remarks>Only there so the so the Designer works properly</remarks>
        public TimeTableViewModel() { }
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="dataService"></param>
        public TimeTableViewModel(IDataService<TrackingEntry> dataService) : base(dataService)
        {
            StartBtnText = "Start Timer";
            _timerService = new TimerService();
            _timerService.TimerTick += timer_Elapsed;
            StartTimerCmd = ReactiveCommand.Create(HandleTimerCmd);
            DeleteRowCmd = ReactiveCommand.Create<System.Collections.IList>(HandleDeleteRow);
            LoadRowsCmd = ReactiveCommand.Create(HandleLoadRows);
            SaveRowsCmd = ReactiveCommand.Create<System.Collections.IList>(HandleSaveRows, this.ObservableForProperty(x => x.Items.Count).Select(_ => Items.Any()));
        }
        #endregion

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
            if (!_timerService.Enabled)
            {
                _timerService.Start();
                StartBtnText = "Stop Timer";
            }
            else
            {
                _timerService.Stop();
                StartBtnText = "Start Timer";
                AddEntry();
            }
            IsRunning = _timerService.Enabled;
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
                    DataService?.SaveEntry(item);
            }
        }

        #endregion

        #region HandleLoadRows

        void HandleLoadRows()
        {
            if (DataService == null)
                return;
            Items.RemoveMany(Items.Where(x => x.Id != Guid.Empty));
            Items.AddRange(DataService.LoadEntries(ProjectName));
        }

        #endregion

        #endregion



        private int elapsedTime;
        private void timer_Elapsed(object? sender, EventArgs e)
        {
            elapsedTime += 1;
            TimerText = TimeSpan.FromSeconds(elapsedTime).ToString(@"hh\:mm\:ss");
        }
        #endregion

    }
}
