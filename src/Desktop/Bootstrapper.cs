using Splat;
using System;
using ProjectTracker.Desktop.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProjectTracker.Desktop.Util.Config;
using Avalonia.Controls;
using ProjectTracker.Desktop.Services;
using ProjectTracker.Desktop.Data;
using ProjectTracker.Services.Abstractions;
using ProjectTracker.Models;

namespace Desktop
{
    public static class Bootstrapper
    {
        public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register(() => new AppConfigService("ProjectTracker.Config"));
            services.AddGooogle(resolver);
            services.AddDb(resolver);
            services.Register<IDataService<TrackingEntry>>(() => new TrackingEntryService(resolver.GetRequiredService<AppDbContext>()));
            services.Register<IDataService<Project>>(() => new ProjectsService(resolver.GetRequiredService<AppDbContext>()));

            services.Register(() => new TimeTableViewModel(resolver.GetRequiredService<IDataService<TrackingEntry>>(), resolver.GetRequiredService<ISheetsService>()));
            services.Register(() => new ProjectsTableViewModel(resolver.GetRequiredService<IDataService<Project>>()));
            services.Register(() => new SettingsTabViewModel(resolver.GetRequiredService<AppConfigService>()));

            services.Register(() => new MainWindowViewModel(resolver.GetRequiredService<ProjectsTableViewModel>(), resolver.GetRequiredService<SettingsTabViewModel>()));
        }

        private static void AddDb(this IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton(() => new AppDbContext());
            if (Design.IsDesignMode)
                return;
            var dbContext = resolver.GetRequiredService<AppDbContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
        }

        private static async void AddGooogle(this IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            var config = resolver.GetRequiredService<AppConfigService>();
            if (await config.GetAsync<bool>("google_enabled"))
            {
                var service = new GoogleSheetsService();

                if (!Design.IsDesignMode)
                    await service.Init();
                services.Register<ISheetsService>(() => service);
            }
        }

        public static TService GetRequiredService<TService>(this IReadonlyDependencyResolver resolver)
        {
            var service = resolver.GetService<TService>();
            if (service == null)
            {
                throw new InvalidOperationException($"Failed to resolve object of type {typeof(TService)}");
            }
            return service;
        }
    }
}