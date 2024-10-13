using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Entities
{
    public class Contato : BaseEntitie
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome não é válido.")]
        public string Nome { get; set; } = null!;

        public long DDDId { get; set; }

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [Phone(ErrorMessage = "O número de telefone não é válido.")]
        public string NrTelefone { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O número de DDD é obrigatório.")]
        public DDD DDD { get; set; }

        public void SetDDDId(long dddId)
        {
            DDDId = dddId;
        }
        public void SetDDD(DDD ddd)
        {
            DDD = ddd;
        }
    }
}