using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechChallenge.Entities;
using TechChallenge.Entities.Requests;
using TechChallenge.Interface.Interfaces;

namespace TechChallenge.Interface.Pages.Contatos
{
    public class CreateModel : PageModel
    {
        private readonly IContatoService _contatoService;
        private readonly IDDDService _idDDService;

        public CreateModel(IContatoService contatoService, IDDDService idDDService)
        {
            _contatoService = contatoService;
            _idDDService = idDDService;
        }

        [BindProperty]
        public RegistrarContatoRequest ContatoRequest { get; set; }
        public List<DDD> DDDList { get; set; } = [];
        public List<SelectListItem> DDDs { get; set; } = [];

        public async Task<IActionResult> OnGet()
        {
            await LoadDDDs();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDDDs();
                return Page();
            }

            try
            {
                await _contatoService.CadastrarContato(ContatoRequest);
                TempData["AlertSuccess"] = "Novo contato registrado com sucesso!";
                return RedirectToPage("/Contatos/Index");
            }
            catch (Exception ex)
            {
                TempData["AlertDanger"] = $"{ex.Message}";
                await LoadDDDs();
                return Page();
            }

        }

        private async Task LoadDDDs()
        {
            DDDList = await _idDDService.ObterTodosDDDs();

            DDDs.Add(new SelectListItem { Value = "", Text = "" });

            if (DDDList != null && DDDList.Any())
            {
                DDDs.AddRange(DDDList.Select(d => new SelectListItem
                {
                    Value = d.NrDDD.ToString(),
                    Text = d.NrDDD.ToString()
                }));
            }

            ViewData["NrDDD"] = DDDs;
        }
    }

}
