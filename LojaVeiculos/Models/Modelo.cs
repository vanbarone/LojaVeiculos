using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using System.Text.Json.Serialization;

namespace LojaVeiculos.Models
{
    public class Modelo
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [StringLength(30)]
        public string Carroceria { get; set; }

        [Required]
        [StringLength(30)]
        public string Motor { get; set; }

        [Required]
        [StringLength(30)]
        public string Cambio { get; set; }

        [Required]
        [StringLength(30)]
        public string TbCombustivel { get; set; }

        [Required]
        public int QtdePortas { get; set; }


        [ForeignKey("Marca")]
        public int IdMarca { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Marca Marca { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public ICollection<Veiculo> Veiculos { get; set; }
    }
}
