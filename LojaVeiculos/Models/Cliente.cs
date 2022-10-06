using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVeiculos.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool TermoUso { get; set; }


        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Cartao> Cartaos { get; set; }

    }
}
