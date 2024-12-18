using TechChallenge.Entities.Requests;

namespace TechChallenge.Interface.Entities
{
    public class ContatoCommand : RegistrarContatoRequest
    {
        public long? Id { get; set; }
        public string? Evento { get; set; }
    }
}
