using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ProjectTracker.Desktop.ViewModels;

namespace ProjectTracker.Desktop.Views
{
    public partial class ProjectsTableView : UserControl
    {
        public ProjectsTableView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
