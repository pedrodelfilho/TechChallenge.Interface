using RabbitMq.Nuget;

namespace TechChallenge.Interface.Extensions
{
    public delegate IRabbitMessageQueue MessageQueueResolver(string fila);
}
