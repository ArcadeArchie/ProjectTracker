using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProjectTracker.Desktop.ViewModels;

namespace ProjectTracker.Desktop.Views
{
    public partial class TimeTable : UserControl
    {
        public TimeTable()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
