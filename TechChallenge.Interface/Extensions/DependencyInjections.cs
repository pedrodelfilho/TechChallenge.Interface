using Microsoft.Extensions.Options;
using RabbitMq.Nuget;
using TechChallenge.Interface.Interfaces;
using TechChallenge.Interface.Services;

namespace TechChallenge.Interface.Extensions
{
    public static class DependencyInjections
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiSettings>(configuration.GetSection("ApiBackEnd"));
            services.Configure<RabbitConfig>(configuration.GetSection("RabbitConfig"));

            //Rabbit Message Queue
            services.AddTransient<IRabbitMessageQueue, RabbitMessageQueue>(sp => {
                var rabbitConfig = configuration.GetSection("RabbitConfig").Get<RabbitConfig>();

                var a = new RabbitMessageQueue(
                    RabbitConfig.Servidor,
                    rabbitConfig?.VHost,
                    RabbitConfig.Usuario,
                    RabbitConfig.Senha,
                    string.Empty,
                    false,
                    rabbitConfig?.FilaRabbit
                );
                return a;
            });


            // Services
            services.AddTransient<IMensageria, Mensageria>();

            services.AddHttpClient<IContatoService, ContatoService>((sp, client) =>
            {
                var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
                client.BaseAddress = new Uri(ApiSettings.Url);
                client.DefaultRequestHeaders.Add("Resource", settings.ResourceContato);
            });

            services.AddHttpClient<IDDDService, DDDService>((sp, client) =>
            {
                var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
                client.BaseAddress = new Uri(ApiSettings.Url);
                client.DefaultRequestHeaders.Add("Resource", settings.ResourceDDD);
            });


            return services;
        }
    }
}
