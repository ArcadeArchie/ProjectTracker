using Splat;
using System;
using ProjectTracker.Desktop.ViewModels;
using ProjectTracker.Desktop.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProjectTracker.Desktop.Util.Config;
using Avalonia.Controls;
using ProjectTracker.Desktop.Services;
using ProjectTracker.Desktop.Services.Interfaces;
using ProjectTracker.Desktop.Data;

namespace Desktop
{
    public static class Bootstrapper
    {
        public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register(() => new AppConfigService("ProjectTracker.Config"));
            services.AddDb(resolver);
            services.Register<IDataService<TrackingEntry>>(() => new TrackingEntryService(resolver.GetRequiredService<AppDbContext>()));
            services.Register<IDataService<Project>>(() => new ProjectsService(resolver.GetRequiredService<AppDbContext>()));

            services.Register(() => new TimeTableViewModel(resolver.GetRequiredService<IDataService<TrackingEntry>>()));
            services.Register(() => new ProjectsTableViewModel(resolver.GetRequiredService<IDataService<Project>>()));

            services.Register(() => new MainWindowViewModel(resolver.GetRequiredService<ProjectsTableViewModel>()));
        }

        private static void AddDb(this IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register(() => new AppDbContext());
            if (Design.IsDesignMode)
                return;
            var dbContext = resolver.GetRequiredService<AppDbContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
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