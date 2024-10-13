using TechChallenge.Interface.Interfaces;
using TechChallenge.Interface.Services;
using Microsoft.Extensions.Options;

namespace TechChallenge.Interface.Extensions
{
    public static class DependencyInjections
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<ApiSettings>(configuration.GetSection("ApiBackEnd"));

            // Services
            services.AddHttpClient<IContatoService, ContatoService>((sp, client) =>
            {
                var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
                client.BaseAddress = new Uri(settings.Url);
                client.DefaultRequestHeaders.Add("Resource", settings.ResourceContato);
            });
            services.AddHttpClient<IDDDService, DDDService>((sp, client) =>
            {
                var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
                client.BaseAddress = new Uri(settings.Url);
                client.DefaultRequestHeaders.Add("Resource", settings.ResourceDDD);
            });


            return services;
        }
    }
}
