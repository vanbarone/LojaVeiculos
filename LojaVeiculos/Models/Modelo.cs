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
        public string NomeModelo { get; set; }

        [Required]
        [StringLength(30)]
        public string Categoria { get; set; }

        [ForeignKey("Marca")]
        public int IdMarca { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Marca Marca;

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public ICollection<Veiculo> Veiculos { get; set; }
    }
}
