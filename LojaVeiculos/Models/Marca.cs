using System.ComponentModel.DataAnnotations;

namespace LojaVeiculos.Models
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomeMarca { get; set; }
    }
}
