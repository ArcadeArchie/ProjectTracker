using DynamicData;
using ProjectTracker.Desktop.Services;
using ProjectTracker.Models;
using ProjectTracker.Services.Abstractions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace ProjectTracker.Desktop.ViewModels
{
    public class TimeTableViewModel : ListViewModelBase<TrackingEntry>
    {
        private readonly ISheetsService _sheetsService;
        private readonly TimerService _timerService;
        #region Properties

        public Project CurrentProject { get; set; }

        /// <summary>
        /// Current ProjectName
        /// </summary>
        public string ProjectName => CurrentProject.Name;

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

        [Reactive]
        public string HourTotal { get; set; }

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
        public ReactiveCommand<Unit, Unit> StartTimerCmd { get; private set; }

        /// <summary>
        /// Command for the DeleteRows button
        /// </summary>
        public ReactiveCommand<System.Collections.IList, Unit> DeleteRowCmd { get; private set; }

        /// <summary>
        /// Command for the LoadRows button
        /// </summary>
        public ReactiveCommand<Unit, Unit> LoadRowsCmd { get; private set; }
        /// <summary>
        /// Command for the LoadRows button
        /// </summary>
        public ReactiveCommand<Unit, Unit> SyncRowsToGoogleCmd { get; private set; }

        /// <summary>
        /// Command for the SaveRows button
        /// </summary>
        public ReactiveCommand<System.Collections.IList, Unit> SaveRowsCmd { get; private set; }

        #endregion

        #region Constructors

        /// <summary></summary>
        /// <remarks>Only there so the so the Designer works properly</remarks>
        public TimeTableViewModel() { }
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="dataService"></param>
        public TimeTableViewModel(IDataService<TrackingEntry> dataService, ISheetsService sheetsService) : base(dataService)
        {
            StartBtnText = "Start Timer";
            _timerService = new TimerService();
            _timerService.TimerTick += Timer_Elapsed;
            _sheetsService = sheetsService;
            CreateCommands();
        }
        #endregion

        #region Methods

        void CreateCommands()
        {
            StartTimerCmd = ReactiveCommand.Create(HandleTimerCmd);
            DeleteRowCmd = ReactiveCommand.Create<System.Collections.IList>(HandleDeleteRow);
            LoadRowsCmd = ReactiveCommand.Create(HandleLoadRows);
            SyncRowsToGoogleCmd = ReactiveCommand.Create(HandleSyncRowsToGoogle, this.ObservableForProperty(x => x.Items.Count).Select(_ => Items.Any()));
            SaveRowsCmd = ReactiveCommand.Create<System.Collections.IList>(HandleSaveRows, this.ObservableForProperty(x => x.Items.Count).Select(_ => Items.Any()));
            Items.CollectionChanged += (o,e) => 
            { 
                var durationSum = Items.Where(x => !x.BeenPaid).Select(x => x.Duration).Aggregate(TimeSpan.Zero, (t1, t2) => t1 + t2); 
                HourTotal = durationSum.ToString(@"hh\,mm");
            };
        }

        #region CommandActions

        #region AddEntry
        /// <summary>
        /// 
        /// </summary>
        private void AddEntry()
        {
            var entry = new TrackingEntry
            {
                Project = CurrentProject,
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
                elapsedTime = 0;
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
                if (item.Id == Guid.Empty)
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
            var entries = DataService.LoadEntries(ProjectName);
            if (entries.Any())
            {
                Items.AddRange(entries);
            }
        }

        #endregion

        #region HandleSyncRowsToGoogle
        private void HandleSyncRowsToGoogle()
        {
            var googleData = _sheetsService.LoadTrackingEntries();
            if (!googleData.Any())
            {
                List<IList<object>> saves = new();
                foreach (TrackingEntry trackingEntry in Items.Where(x => x.Id != Guid.Empty).OrderBy(x => x.TimeStamp))
                    saves.Add(new List<object>()
                    {
                        trackingEntry.ProjectName,
                        trackingEntry.TimeStamp,
                        trackingEntry.Duration,
                        false,
                        trackingEntry.Id
                    });
                _sheetsService.Export(saves);
            }
            else
            {
                var googleIds = googleData.Where(x => x.Id != Guid.Empty).Select(x => x.Id);

                List<IList<object>> saves = new();
                foreach (TrackingEntry trackingEntry in Items.Where(x => !googleIds.Contains(x.Id)).OrderBy(x => x.TimeStamp))
                {
                    saves.Add(new List<object>()
                    {
                        trackingEntry.ProjectName,
                        trackingEntry.TimeStamp,
                        trackingEntry.Duration,
                        trackingEntry.BeenPaid,
                        trackingEntry.Id
                    });
                }
                _sheetsService.Export(saves, googleData.Where(x => x.Id != Guid.Empty).Count());
            }

        }
        #endregion

        #endregion



        private int elapsedTime;
        private void Timer_Elapsed(object? sender, EventArgs e)
        {
            elapsedTime += 1;
            TimerText = TimeSpan.FromSeconds(elapsedTime).ToString(@"hh\:mm\:ss");
        }
        #endregion

    }
}
