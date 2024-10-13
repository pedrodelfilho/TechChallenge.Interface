using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Entities
{
    public class DDD : BaseEntitie
    {
        [Required]
        public byte NrDDD { get; set; }

        public ICollection<Contato> Contatos { get; set; } = new List<Contato>();

        public static DDD SetDDD(long dddId, byte nrDDD)
        {
            return new DDD
            {
                NrDDD = nrDDD,
                Id = dddId
            };
        }
    }
}
