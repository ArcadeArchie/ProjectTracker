using ProjectTracker.Services.Abstractions;
using System.Collections.ObjectModel;

namespace ProjectTracker.Desktop.ViewModels
{
    public abstract class ListViewModelBase<TEntity> : ViewModelBase where TEntity : class
    {
        protected virtual IDataService<TEntity> DataService { get; }
        public virtual ObservableCollection<TEntity> Items { get; set; }

        protected ListViewModelBase() { }
        protected ListViewModelBase(IDataService<TEntity> dataService)
        {
            DataService = dataService;
        }
    }
}
