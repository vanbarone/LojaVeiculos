﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVeiculos.Models
{
    public class ItemVenda
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]     //não mostra esse campo no json na inserção e alteração
        public int Id { get; set; }

        [Required]
        [ForeignKey("Veiculo")]
        public int IdVeiculo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Veiculo veiculo { get; set; }

        [Required]
        [ForeignKey("Venda")]
        public int IdVenda { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Venda venda { get; set; }
    }
}
