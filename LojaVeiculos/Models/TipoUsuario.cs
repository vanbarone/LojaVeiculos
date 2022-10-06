using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVeiculos.Models
{
    public class TipoUsuario
    {
        [JsonIgnore(Condition=JsonIgnoreCondition.Never)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Tipo { get; set; }
    }
}
