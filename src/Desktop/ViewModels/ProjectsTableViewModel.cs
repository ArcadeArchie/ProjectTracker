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
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Desktop.ViewModels
{
    public class ProjectsTableViewModel : ListViewModelBase<Project>
    {
        [Reactive]
        public string NewProjectName { get; set; }

        public ReactiveCommand<Unit, Unit> CreateProjectCmd { get; }

        public override ObservableCollection<Project> Items { get; set; } = new ObservableCollection<Project>(new[] { new Project { Name = "No Projects" } });

        protected override ProjectsService DataService { get => (ProjectsService)base.DataService; }

        public ProjectsTableViewModel() { }

        public ProjectsTableViewModel(IDataService<Project> dataService) : base(dataService)
        {
            CreateProjectCmd = ReactiveCommand.Create(HandleCreateProject, this.ObservableForProperty(x => x.NewProjectName).Select(_ => !String.IsNullOrWhiteSpace(NewProjectName)));
        }

        private void HandleCreateProject()
        {
            throw new NotImplementedException();
        }
    }
}
