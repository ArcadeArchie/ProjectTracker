using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.FreeDesktop.DBusIme;
using Desktop;
using DynamicData;
using DynamicData.Binding;
using ProjectTracker.Desktop.Models;
using ProjectTracker.Desktop.Services;
using ProjectTracker.Desktop.Services.Interfaces;
using ProjectTracker.Desktop.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Desktop.ViewModels
{
    public class ProjectsTableViewModel : ListViewModelBase<Project>
    {
        [Reactive]
        public Project SelectedItem { get; set; }

        [Reactive]
        public string NewProjectName { get; set; }

        public ReactiveCommand<Unit, Unit> CreateProjectCmd { get; }
        public ReactiveCommand<Project, Unit> OpenProjectCmd { get; }

        public override ObservableCollection<Project> Items { get; set; } = new ObservableCollection<Project>();

        public ProjectsTableViewModel() { }

        public ProjectsTableViewModel(IDataService<Project> dataService) : base(dataService)
        {
            this.WhenValueChanged(x => x.SelectedItem).Subscribe(HandleSelectionChange);
            CreateProjectCmd = ReactiveCommand.Create(HandleCreateProject, this.ObservableForProperty(x => x.NewProjectName).Select(_ => !String.IsNullOrWhiteSpace(NewProjectName)));
            OpenProjectCmd = ReactiveCommand.Create<Project>(HandleOpenProject);
            LoadData().Subscribe((x) => { Items.Add(x); });
        }

        private void HandleOpenProject(Project project)
        {
            var viewModel = Locator.Current.GetRequiredService<TimeTableViewModel>();
            viewModel.CurrentProject = project;
            var window = new MainWindow() 
            {
                Content = new TimeTable()
                {
                    DataContext = viewModel
                }
            };
            window.ShowDialog((App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow);
        }

        private IObservable<Project> LoadData()
        {
            return Observable.Create<Project>(async observer => 
            {
                IEnumerable<Project> entries = await DataService.LoadEntriesAsync();
                foreach (Project entry in entries)
                {
                    observer.OnNext(entry);
                }
            });
        }

        private void HandleSelectionChange(Project? selectedProject)
        {
            if (selectedProject != null)
            {
                OpenProjectCmd.Execute(selectedProject).Subscribe();
            }
        }

        private void HandleCreateProject()
        {
            var newProject = DataService.SaveEntry(new Project { Name = NewProjectName });
            Items.Add(newProject.Entity);
        }
    }
}
