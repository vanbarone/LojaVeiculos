using LojaVeiculos.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LojaVeiculos.Models
{
    public class Veiculo
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]     //não mostra esse campo no json na inserção e alteração
        public int Id { get; set; }

        [StringLength(7)]
        public string Placa { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]
        [StringLength(30)]
        public VeiculoEnum.Cor Cor { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,1)")]
        public decimal Km { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }

        [Required]
        [StringLength(30)]
        public VeiculoEnum.TpCombustivel TpCombustivel { get; set; }

        [Required]
        [StringLength(30)]
        public VeiculoEnum.Status Status { get; set; }

        [Required]
        [ForeignKey("Concessionaria")]
        public int IdConcessionaria { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Concessionaria concessionaria { get; set; }

        [Required]
        [ForeignKey("Modelo")]
        public int IdModelo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Modelo Modelo { get; set; }
    }
}
