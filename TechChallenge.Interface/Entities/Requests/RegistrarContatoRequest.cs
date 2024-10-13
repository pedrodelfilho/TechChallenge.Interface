using TechChallenge.Validation;
using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Entities.Requests
{
    public class RegistrarContatoRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Nome(2, ErrorMessage = "O nome deve conter nome e sobrenome.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O número de DDD é obrigatório.")]
        public byte NrDDD { get; set; }

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [NrTelefone(ErrorMessage = "O número de telefone deve estar no formato 9XXXXXXXX.")]
        public string NrTelefone { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        public string Email { get; set; } = null!;
    }
}
