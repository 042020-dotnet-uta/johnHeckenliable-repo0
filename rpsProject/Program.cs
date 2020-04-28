using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

//Ryan Shereda, JD Heckenliable, Michael Hall
namespace rpsProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            //{
                GamePlay game = serviceProvider.GetService<GamePlay>();
                game.StartGame();
            //}
        }
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging((configure) =>
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Error);
            })
            .AddTransient<GamePlay>();
        }
    }
}
