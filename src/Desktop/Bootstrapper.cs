using Splat;
using Desktop.Data;
using Desktop.Services;
using System;
using ProjectTracker.ViewModels;
using Desktop.ViewModels.Interfaces;
using ProjectTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Desktop
{
    public static class Bootstrapper
    {
        public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.AddDb(resolver);
            services.Register<IDataService<TrackingEntry>>(() => new TrackingEntryService(resolver.GetRequiredService<AppDbContext>()));
            services.Register<ITimeTableViewModel>(() => new TimeTableViewModel(resolver.GetRequiredService<IDataService<TrackingEntry>>()));
            services.Register<MainWindowViewModel>(() => new MainWindowViewModel(resolver.GetRequiredService<ITimeTableViewModel>()));
        }

        private static void AddDb(this IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register<AppDbContext>(() => new AppDbContext());
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