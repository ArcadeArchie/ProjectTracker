using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProjectTracker.ViewModels;

namespace ProjectTracker.Views
{
    public partial class TimeTable : UserControl
    {
        public TimeTable()
        {
            InitializeComponent();
            DataContext = new TimeTableViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
