using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Desktop;
using ProjectTracker.Desktop.ViewModels;
using ProjectTracker.Desktop.Views;
using Splat;

namespace ProjectTracker.Desktop
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                DataContext = Locator.Current.GetRequiredService<MainWindowViewModel>();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = DataContext,
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}