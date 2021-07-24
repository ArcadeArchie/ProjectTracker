using ProjectTracker.Desktop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
