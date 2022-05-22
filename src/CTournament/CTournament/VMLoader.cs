using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament
{
    public interface ITransient { }
    public interface ISingleton { }

    public class VMLoader
    {
        private static IServiceProvider _serviceProvider;

        public static void Init()
        {
            var services = new ServiceCollection();

            services.Scan(el =>
                el.FromAssemblyOf<ISingleton>()
                    .AddClasses(cl => cl.AssignableTo<ISingleton>()).AsSelf().WithSingletonLifetime()
                );
            services.Scan(el =>
                 el.FromAssemblyOf<ITransient>()
                     .AddClasses(cl => cl.AssignableTo<ITransient>()).AsSelf().WithTransientLifetime()
                 );

            _serviceProvider = services.BuildServiceProvider();

            foreach (ServiceDescriptor itemService in services)
                _serviceProvider.GetRequiredService(itemService.ServiceType);
        }

        public static T? Resolve<T>() => _serviceProvider.GetService<T>();

        public ViewModels.MainWindow? MainWindow => Resolve<ViewModels.MainWindow>();
        public ViewModels.TournamentDirectory? TournamentDirectory => Resolve<ViewModels.TournamentDirectory>();
        public ViewModels.TournamentReplays? TournamentReplays => Resolve<ViewModels.TournamentReplays>();
        public ViewModels.UserStatistics? UserStatistics => Resolve<ViewModels.UserStatistics>();
        public ViewModels.RateSettings? RateSettings => Resolve<ViewModels.RateSettings>();
        public ViewModels.CrashApp? CrashApp => Resolve<ViewModels.CrashApp>();

        public ViewModels.UserStatisticsItem? UserStatisticsItem => Resolve<ViewModels.UserStatisticsItem>();
    }
}
