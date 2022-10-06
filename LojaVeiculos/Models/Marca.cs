using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVeiculos.Models
{
    public class Marca
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]        
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string NomeMarca { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public ICollection<Modelo> Modelos { get; set; }
    }
}
