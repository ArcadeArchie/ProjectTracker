using Avalonia.Controls;
using Desktop;
using DynamicData;
using ProjectTracker.Desktop.Views;
using ProjectTracker.Models;
using ProjectTracker.Services.Abstractions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

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
        public ReactiveCommand<Project, Unit> DeleteProjectCmd { get; }
        
        public override ObservableCollection<Project> Items { get; set; } = new ObservableCollection<Project>();

        public ProjectsTableViewModel() { }

        public ProjectsTableViewModel(IDataService<Project> dataService) : base(dataService)
        {
            CreateProjectCmd = ReactiveCommand.Create(HandleCreateProject, this.ObservableForProperty(x => x.NewProjectName).Select(_ => !String.IsNullOrWhiteSpace(NewProjectName)));
            OpenProjectCmd = ReactiveCommand.Create<Project>(HandleOpenProject);
            DeleteProjectCmd = ReactiveCommand.Create<Project>(HandleDeleteProject);
            LoadData().Subscribe((x) => { Items.Add(x); });
        }

        private void HandleOpenProject(Project project)
        {
            TimeTableViewModel viewModel = Locator.Current.GetRequiredService<TimeTableViewModel>();
            viewModel.CurrentProject = project;
            Window window = new()
            {
                Content = new TimeTable()
                {
                    DataContext = viewModel
                }
            };
            window.Show();
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

        private void HandleCreateProject()
        {
            var newProject = DataService.SaveEntry(new Project { Name = NewProjectName });
            Items.Add(newProject.Entity);
        }
        private void HandleDeleteProject(Project project)
        {
            Items.Remove(project);
            DataService.DeleteEntry(project);
        }
    }
}
