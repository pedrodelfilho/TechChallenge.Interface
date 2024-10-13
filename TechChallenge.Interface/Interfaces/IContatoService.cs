using TechChallenge.Entities;
using TechChallenge.Entities.Requests;

namespace TechChallenge.Interface.Interfaces
{
    public interface IContatoService
    {
        Task<Contato> CadastrarContato(RegistrarContatoRequest request);
        Task RemoverContato(long id);
        Task<Contato> AtualizarContato(AtualizarContatoRequest request);
        Task<List<Contato>> ObterTodosContatos();
        Task<Contato> ObterContatoPorId(long id);
    }
}
