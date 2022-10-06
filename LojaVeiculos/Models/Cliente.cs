using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LojaVeiculos.Models
{
    public class Cliente
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [Key]
        public int Id { get; set; }

        [Required]
        public bool TermoUso { get; set; }


        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Usuario Usuario { get; set; }

        public ICollection<Cartao> Cartoes { get; set; }

    }
}
