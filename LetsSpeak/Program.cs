using Microsoft.Extensions.DependencyInjection;
using Repository;
using Services;

namespace LetsSpeak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddRepository()
                .AddServices();

            var serviceProvider = services.BuildServiceProvider();
            var menuService = serviceProvider.GetService<MenuService>();
            menuService.InitializeMenu();
        }
    }
}