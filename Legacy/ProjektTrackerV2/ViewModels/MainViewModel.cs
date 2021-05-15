// Decompiled with JetBrains decompiler
// Type: ProjektTrackerV2.ViewModels.MainViewModel
// Assembly: ProjektTrackerV2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DCADB393-07CC-49DC-85B8-59DC6B5FC974
// Assembly location: C:\Users\nicob\source\repos\Git\GAB\ProjektTrackerV2\ProjektTrackerV2\bin\Debug\netcoreapp3.1\ProjektTrackerV2.dll

using ProjektTrackerV2.Connectors.Google;
using ProjektTrackerV2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace ProjektTrackerV2.ViewModels
{
  public class MainViewModel : ViewModelBase
  {
    private DispatcherTimer _timer;
    private MainWindow parent;
    private DateTime _timerStartTime;
    private bool _started;
    private GoogleConnector _googleConnector;
    private ICommand _timerBtnCmd;
    private ICommand _saveDataBtnCmd;
    private ICommand _loadDataBtnCmd;

    public ObservableCollection<TrackingEntry> Items { get; }

    public ObservableCollection<SheetType> SheetTypes { get; }

    public ICommand TimerBtnCmd
    {
      get
      {
        return this._timerBtnCmd ?? (this._timerBtnCmd = (ICommand) new CommandHandler((Action) (() => this.OnTimerBtn()), (Func<bool>) (() => this.CanExecute)));
      }
    }

    public ICommand SaveDataBtnCmd
    {
      get
      {
        return this._saveDataBtnCmd ?? (this._saveDataBtnCmd = (ICommand) new CommandHandler((Action) (() => this.SaveDataToGoogle_Click()), (Func<bool>) (() => this.CanExecute)));
      }
    }

    public ICommand LoadDataBtnCmd
    {
      get
      {
        return this._loadDataBtnCmd ?? (this._loadDataBtnCmd = (ICommand) new CommandHandler((Action) (() => this.LoadDataFromGoogle_Click()), (Func<bool>) (() => this.CanExecute)));
      }
    }

    public MainViewModel()
    {
      DispatcherTimer dispatcherTimer = new DispatcherTimer();
      dispatcherTimer.set_Interval(new TimeSpan(0, 0, 1));
      this._timer = dispatcherTimer;
      // ISSUE: explicit constructor call
      base.\u002Ector();
      this.Items = new ObservableCollection<TrackingEntry>(new List<TrackingEntry>()
      {
        new TrackingEntry()
        {
          ProjectName = "",
          TimeStamp = DateTimeOffset.Now,
          BeenPaid = false
        }
      });
    }

    public MainViewModel(MainWindow mainWindow)
    {
      DispatcherTimer dispatcherTimer = new DispatcherTimer();
      dispatcherTimer.set_Interval(new TimeSpan(0, 0, 1));
      this._timer = dispatcherTimer;
      // ISSUE: explicit constructor call
      base.\u002Ector();
      this._timer.add_Tick(new EventHandler(this.timer_Tick));
      this.parent = mainWindow;
      this._googleConnector = new GoogleConnector();
      this.Items = new ObservableCollection<TrackingEntry>(this._googleConnector.GetEntries());
      this.SheetTypes = new ObservableCollection<SheetType>((IEnumerable<SheetType>) new SheetType[2]
      {
        new SheetType()
        {
          Description = "Use Google",
          IsSelected = true
        },
        new SheetType()
        {
          Description = "Use Excel",
          IsSelected = false
        }
      });
    }

    private bool CanExecute
    {
      get
      {
        return true;
      }
    }

    private void OnTimerBtn()
    {
      if (this._started)
      {
        this.parent.StartTimerBtn.Content = (object) "Start";
        this.Items.Add(new TrackingEntry()
        {
          TimeStamp = DateTimeOffset.Now,
          Duration = DateTime.Now.Subtract(this._timerStartTime),
          ProjectName = this.parent.ProjectNameBox.Text
        });
        this._timer.Stop();
        this._started = false;
      }
      else
      {
        this.parent.StartTimerBtn.Content = (object) "Stop";
        this._timerStartTime = DateTime.Now;
        this._timer.Start();
        this._started = true;
      }
    }

    private void SaveDataToGoogle_Click()
    {
      List<IList<object>> saves = new List<IList<object>>();
      foreach (TrackingEntry trackingEntry in (Collection<TrackingEntry>) this.Items)
        saves.Add((IList<object>) new List<object>()
        {
          (object) trackingEntry.ProjectName,
          (object) trackingEntry.TimeStamp,
          (object) trackingEntry.Duration,
          (object) false
        });
      this._googleConnector.WriteDataToGoogle(saves);
    }

    private void LoadDataFromGoogle_Click()
    {
      this.Items.Clear();
      foreach (TrackingEntry entry in this._googleConnector.GetEntries())
        this.Items.Add(entry);
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      this.parent.TimerTextBox.Text = this._timerStartTime.Subtract(DateTime.Now).ToString().Replace("-", "").Remove(8, 8);
    }
  }
}
