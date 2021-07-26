using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ProjectTracker.Desktop.ViewModels;
using ReactiveUI;

namespace ProjectTracker.Desktop.Views
{
    public partial class TimeTable : ReactiveUserControl<TimeTableViewModel>
    {
        public TimeTable()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
