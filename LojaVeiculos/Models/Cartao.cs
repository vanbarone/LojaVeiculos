using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LojaVeiculos.Models
{
    public class Cartao
    {
        [Key]
        [Required(ErrorMessage = "Insira o numero do cartao")]
        [StringLength(20)]
        //[RegularExpression("4[0-9]{12}(?:[0-9]{3})", ErrorMessage = "Insira um numero valido")]
        public string Numero { get; set; }

        
        [Required(ErrorMessage = "Insira o nome do titular")]
        [StringLength(60)]
        public string Titular { get; set; }


        [Required]
        [StringLength(20)]
        public string Bandeira { get; set; }


        [Required(ErrorMessage = "Insira o cpf do titular")]
        [StringLength(15)]
        public string Cpf { get; set; }


        [Required]
        public int MesVencimento { get; set; }


        [Required]
        public int AnoVencimento { get; set; }


        [Required]
        [StringLength(5)]
        public string CodSeguranca { get; set; }


        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public Cliente Cliente { get; set; }
    }
}
