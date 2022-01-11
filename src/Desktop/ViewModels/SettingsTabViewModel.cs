using ProjectTracker.Config;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using System.Threading.Tasks;

namespace ProjectTracker.Desktop.ViewModels
{
    public class SettingsTabViewModel : ViewModelBase
    {
        private readonly AppConfig _appConfig;
        [Reactive]
        public bool IsGoogleEnabled { get; set; }

        public ReactiveCommand<Unit, Unit> SaveChangesCmd { get; }

        public SettingsTabViewModel()
        {

        }
        public SettingsTabViewModel(AppConfig appConfig)
        {
            _appConfig = appConfig;
            Init();
            SaveChangesCmd = ReactiveCommand.CreateFromTask(HandleSaveChangesAsync);
        }

        private void Init()
        {
            IsGoogleEnabled = _appConfig.GoogleEnabled;
        }

        private async Task HandleSaveChangesAsync()
        {
            if (IsGoogleEnabled != _appConfig.GoogleEnabled)
            {
                _appConfig.GoogleEnabled = IsGoogleEnabled;
            }
        }
    }
}
