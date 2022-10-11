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
        public int Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [Required]
        public DateTime Data { get; set; }


        [Required]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Cliente Cliente { get; set; }

        public ICollection<ItemVenda> ItensVenda { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal VlTotal { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string FormaPagto { get; set; }


        [Required]
        [StringLength(255)]
        public string CartaoTitular { get; set; }


        [Required(ErrorMessage = "Insira o numero do cartao")]
        [StringLength(20)]
        //[RegularExpression("4[0-9]{12}(?:[0-9]{3})", ErrorMessage = "Insira um numero valido")]
        public string CartaoNumero { get; set; }


        [Required]
        [StringLength(20)]
        public string CartaoBandeira { get; set; }


        [Required]
        [StringLength(15)]
        public string CartaoCpf { get; set; }


        [Required]
        public int CartaoMesVencimento { get; set; }


        [Required]
        public int CartaoAnoVencimento { get; set; }


        [Required]
        public int CartaoCodSeguranca { get; set; }


       
    }
}
