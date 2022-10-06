using LojaVeiculos.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Text.Json.Serialization;

namespace LojaVeiculos.Models
{
    public class Veiculo
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]     //não mostra esse campo no json na inserção e alteração
        public int Id;

        [StringLength(7)]
        public string Placa;

        [Required]
        public int Ano;

        [Required]
        public VeiculoEnum.Cor Cor;

        [Required]
        [Column(TypeName = "decimal(10,1)")]
        public decimal Km;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor;

        [Required]
        public VeiculoEnum.TpCombustivel TpCombustivel;

        public VeiculoEnum.Status Status;

        [Required]
        [ForeignKey("Concessionaria")]
        public int IdConcessionaria;

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Concessionaria concessionaria;

        [Required]
        [ForeignKey("Modelo")]
        public int IdModelo;

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Modelo modelo;
    }
}
