using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracker.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ProjectsTableViewModel ProjectsTableViewModel { get; }
        public SettingsTabViewModel SettingsTabViewModel { get; }
        public MainWindowViewModel()
        {

        }
        public MainWindowViewModel(ProjectsTableViewModel projectsTableViewModel, SettingsTabViewModel settingsTabViewModel)
        {
            ProjectsTableViewModel = projectsTableViewModel;
            SettingsTabViewModel = settingsTabViewModel;
        }
    }
}
