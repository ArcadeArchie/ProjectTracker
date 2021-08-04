using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ProjectTracker.Desktop.Views
{
    public partial class SettingsTabView : UserControl
    {
        public SettingsTabView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
