using Desktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.ViewModels
{
    public abstract class ListViewModelBase<TEntity> : ViewModelBase where TEntity : class
    {
        protected virtual IDataService<TEntity> _dataService { get; }
        public virtual ObservableCollection<TEntity> Items { get; set; }

        protected ListViewModelBase(IDataService<TEntity> dataService)
        {
            _dataService = dataService;
        }
    }
}
