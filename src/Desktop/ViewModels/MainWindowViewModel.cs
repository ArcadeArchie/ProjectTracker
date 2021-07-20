using System;
using System.Collections.Generic;
using System.Text;
using Desktop.ViewModels.Interfaces;

namespace ProjectTracker.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public ITimeTableViewModel TimeTableViewModel { get; }

        public MainWindowViewModel(ITimeTableViewModel timeTableViewModel)
        {
            TimeTableViewModel = timeTableViewModel;
        }
    }
}
