using Microsoft.AspNetCore.Mvc.RazorPages;
using TechChallenge.Entities;
using TechChallenge.Interface.Interfaces;

namespace TechChallenge.Interface.Pages.Contatos
{
    public class IndexModel(IContatoService contatoService, IDDDService dDDService) : PageModel
    {
        private readonly IContatoService _contatoService = contatoService;
        private readonly IDDDService _dDDService = dDDService;

        public IList<Contato> Contato { get; set; } = default!;
        public List<DDD> DDDs { get; set; } = new();
        public byte? SelectedNrDDD { get; set; }

        public async Task OnGetAsync(byte? NrDDD)
        {
            DDDs = await _dDDService.ObterTodosDDDs();
            SelectedNrDDD = NrDDD;

            if (NrDDD.HasValue && NrDDD > 0)
            {
                Contato = (await _contatoService.ObterTodosContatos())
                          .Where(c => c.DDD.NrDDD == NrDDD)
                          .ToList();
            }
            else
            {
                Contato = await _contatoService.ObterTodosContatos();
            }
        }
    }

}
