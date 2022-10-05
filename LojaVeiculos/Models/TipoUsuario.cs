using System.ComponentModel.DataAnnotations;

namespace LojaVeiculos.Models
{
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Tipo { get; set; }
    }
}
