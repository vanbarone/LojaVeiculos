using LojaVeiculos.Enuns;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

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
        public string CartaoTitular { get; set; }


        [Required(ErrorMessage = "Insira o numero do cartao")]
        [RegularExpression("4[0-9]{12}(?:[0-9]{3})", ErrorMessage = "Insira um numero valido")]
        public string CartaoNumero { get; set; }

        [Required]
        public string CartaoBandeira { get; set; }


        [Required]
        public string CartaoCpf { get; set; }


        [Required]
        public int CartaoMesVencimento { get; set; }


        [Required]
        public string CartaoAnoVencimento { get; set; }


        [Required]
        public string CartaoCodSeguranca { get; set; }


        [Required]
        public int IdCliente;

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [ForeignKey("Cliente")]
        public Cliente cliente;

        [ForeignKey("ItemVenda")]
        public ICollection<ItemVenda> itemVenda;
    }
}
