using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVeiculos.Models
{
    public class ItemCompra
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]     //não mostra esse campo no json na inserção e alteração
        public int Id { get; set; }


        [Required]
        [ForeignKey("Veiculo")]
        public int IdVeiculo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Veiculo Veiculo { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [Required]
        [ForeignKey("Compra")]
        public int IdCompra { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Compra Compra { get; set; }
    }
}
