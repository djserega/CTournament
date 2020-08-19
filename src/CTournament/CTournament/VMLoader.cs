using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTournament
{
    public class VMLoader
    {
        private static ServiceProvider _serviceProvider;

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ViewModels.MainWindow>();
            services.AddSingleton<ViewModels.TournamentDirectory>();
            services.AddSingleton<ViewModels.TournamentReplays>();
            services.AddSingleton<ViewModels.RateSettings>();

            _serviceProvider = services.BuildServiceProvider();

            foreach (var itemService in services)
                _serviceProvider.GetRequiredService(itemService.ServiceType);
        }

        public ViewModels.MainWindow MainWindow => _serviceProvider.GetRequiredService<ViewModels.MainWindow>();
        public ViewModels.TournamentDirectory TournamentDirectory => _serviceProvider.GetRequiredService<ViewModels.TournamentDirectory>();
        public ViewModels.TournamentReplays TournamentReplays => _serviceProvider.GetRequiredService<ViewModels.TournamentReplays>();
        public ViewModels.RateSettings RateSettings => _serviceProvider.GetRequiredService<ViewModels.RateSettings>();
    }
}
