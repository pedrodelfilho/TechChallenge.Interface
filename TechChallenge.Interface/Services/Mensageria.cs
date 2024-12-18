using RabbitMq.Nuget;
using RabbitMq.Nuget.Exceptions;
using TechChallenge.Interface.Interfaces;

namespace TechChallenge.Interface.Services
{
    public class Mensageria : IMensageria
    {
        private readonly IRabbitMessageQueue _messageQueue;
        protected bool isNackEvent = false;

        public Mensageria(IRabbitMessageQueue messageQueue)
        {
            _messageQueue = messageQueue ?? throw new ArgumentNullException(nameof(messageQueue));
        }

        public void PublicarMensagem(string mensagem)
        {
            isNackEvent = false;

            _messageQueue.PutWithConfirmation(mensagem);

            if (isNackEvent) throw new QueueException(new Exception());
        }
    }
}
