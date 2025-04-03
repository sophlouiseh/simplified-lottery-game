using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimplifiedLotteryGame.Configuration;
using SimplifiedLotteryGame.Interfaces;
using SimplifiedLotteryGame.Services;

namespace SimplifiedLotteryGame
{
    class Program
    {

        static void Main()
        {
            // Set up dependency injection and configuration
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var appSettings = new AppSettings();
            configuration.Bind(appSettings);

            var serviceProvider = new ServiceCollection()
                .AddSingleton(appSettings)
                .AddTransient<ILotteryService, LotteryService>()
                .AddTransient<IGameService, GameService>()
                .AddTransient<IConsoleService, ConsoleService>()
                .AddTransient<IPlayerService, PlayerService>()
                .AddTransient<ITicketService, TicketService>()
                .BuildServiceProvider();



            IGameService gameService = serviceProvider.GetRequiredService<IGameService>();
            gameService.RunGame();
        }


    }
}