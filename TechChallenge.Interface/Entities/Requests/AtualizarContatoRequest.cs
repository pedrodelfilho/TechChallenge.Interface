using System.ComponentModel.DataAnnotations;
using TechChallenge.Validation;

namespace TechChallenge.Entities.Requests
{
    public class AtualizarContatoRequest
    {
        [Required(ErrorMessage = "O ID é obrigatório.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome não é válido.")]
        [Nome(2, ErrorMessage = "O nome completo deve conter nome e sobrenome.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O número de DDD é obrigatório.")]
        [Range(10, 99, ErrorMessage = "O DDD deve ter exatamente 2 dígitos.")]
        public byte NrDDD { get; set; }

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [Phone(ErrorMessage = "O número de telefone não é válido.")]
        [NrTelefone(ErrorMessage = "O número de telefone deve estar no formato 9XXXXXXXX.")]
        public string NrTelefone { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        public string Email { get; set; } = null!;
    }
}
