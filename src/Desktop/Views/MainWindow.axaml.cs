using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ProjectTracker.Services;
using System;
using System.Diagnostics;

namespace ProjectTracker.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
