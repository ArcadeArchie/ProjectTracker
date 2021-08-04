using Avalonia.FreeDesktop.DBusIme;
using DynamicData.Binding;
using ProjectTracker.Desktop.Util.Config;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker.Desktop.ViewModels
{
    public class SettingsTabViewModel : ViewModelBase
    {
        private readonly AppConfigService _appConfig;
        [Reactive]
        public bool IsGoogleEnabled { get; set; }

        public ReactiveCommand<Unit, Unit> SaveChangesCmd { get; }

        public SettingsTabViewModel()
        {

        }
        public SettingsTabViewModel(AppConfigService appConfig)
        {
            _appConfig = appConfig;
            Init();
            SaveChangesCmd = ReactiveCommand.CreateFromTask(HandleSaveChangesAsync);
        }

        private async void Init()
        {
            IsGoogleEnabled = await _appConfig.GetAsync<bool>("google_enabled");
        }

        private async Task HandleSaveChangesAsync()
        {
            if (IsGoogleEnabled != await _appConfig.GetAsync<bool>("google_enabled"))
            {
                await _appConfig.SetAsync<bool>("google_enabled", IsGoogleEnabled);
            }
        }
    }
}
