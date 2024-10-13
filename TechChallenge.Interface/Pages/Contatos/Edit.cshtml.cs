using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechChallenge.Entities;
using TechChallenge.Entities.Requests;
using TechChallenge.Interface.Interfaces;

namespace TechChallenge.Interface.Pages.Contatos
{
    public class EditModel : PageModel
    {
        private readonly IContatoService _contatoService;
        private readonly IDDDService _idDDService;

        public EditModel(IContatoService contatoService, IDDDService idDDService)
        {
            _contatoService = contatoService;
            _idDDService = idDDService;
        }

        [BindProperty]
        public AtualizarContatoRequest AtualizarContatoRequest { get; set; } = default;
        public List<DDD> DDDList { get; set; } = [];
        public List<SelectListItem> DDDs { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(long id)
        {
            var contato = await _contatoService.ObterContatoPorId(id);
            if (contato == null)
            {
                return NotFound();
            }

            LoadContato(contato);
            await LoadDDDs();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await LoadDDDs();
                    return Page();
                }

                await _contatoService.AtualizarContato(new AtualizarContatoRequest
                {
                    Id = AtualizarContatoRequest.Id,
                    Nome = AtualizarContatoRequest.Nome,
                    NrTelefone = AtualizarContatoRequest.NrTelefone,
                    Email = AtualizarContatoRequest.Email,
                    NrDDD = AtualizarContatoRequest.NrDDD
                });

                TempData["AlertSuccess"] = "Contato atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["AlertDanger"] = $"Ocorreu um erro, Erro: {ex.Message}";
            }           

            return RedirectToPage("./Index");
        }

        private async Task LoadDDDs()
        {
            DDDList = await _idDDService.ObterTodosDDDs();

            DDDs.Add(new SelectListItem { Value = "", Text = "" });

            if (DDDList.Count > 0)
            {
                DDDs.AddRange(DDDList.Select(d => new SelectListItem
                {
                    Value = d.NrDDD.ToString(),
                    Text = d.NrDDD.ToString()
                }));
            }

            ViewData["NrDDD"] = DDDs;
        }

        private void LoadContato(Contato contato)
        {
            AtualizarContatoRequest = new AtualizarContatoRequest
            {
                Email = contato.Email,
                Id = contato.Id,
                Nome = contato.Nome,
                NrDDD = contato.DDD.NrDDD,
                NrTelefone = contato.NrTelefone
            };
        }
    }
}
