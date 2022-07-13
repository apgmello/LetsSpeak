using Microsoft.Extensions.DependencyInjection;
using Repository.ConcreteClass;
using Repository.Interface;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IEnglishTermRepository, EnglishTermRepository>();
            services.AddScoped<IPortugueseTermRepository, PortugueseTermRepository>();
            return services;
        }
    }

}
