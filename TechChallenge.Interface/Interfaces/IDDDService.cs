using TechChallenge.Entities;

namespace TechChallenge.Interface.Interfaces
{
    public interface IDDDService
    {
        Task<List<DDD>> ObterTodosDDDs();
    }
}
