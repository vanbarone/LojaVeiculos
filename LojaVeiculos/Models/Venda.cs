using LojaVeiculos.Enuns;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System;

namespace LojaVeiculos.Models
{
    public class Venda
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]     //não mostra esse campo no json na inserção e alteração
        public int Id;

        [Required]
        public DateTime Data;

        [Column(TypeName = "decimal(10,2)")]
        public decimal VlTotal;

        public VendaEnum.FormaPagto FormaPagto;

        [Required]
        public int IdCliente;

        [ForeignKey("Cliente")]
        public Cliente cliente;

        [ForeignKey("ItemVenda")]
        public ItemVenda itemVenda;
    }
}
