using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Entities
{
    public abstract class BaseEntitie
    {
        [Key]
        public long Id { get; set; }
    }
}
