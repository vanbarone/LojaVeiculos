using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVeiculos.Models
{
    public class Cartao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o nome do titular")]
        public string Titular { get; set; }


        [Required(ErrorMessage = "Insira o numero do cartao")]
        [RegularExpression("4[0-9]{12}(?:[0-9]{3})", ErrorMessage = "Insira um numero valido")]
        public string Numero { get; set; }


        [Required]
        public string Apelido { get; set; }


        [Required]
        public string Bandeira { get; set; }


        [Required(ErrorMessage = "Insira o nome do titular")]
        public string Cpf { get; set; }


        [Required]
        public int MesVencimento { get; set; }


        [Required]
        public string AnoVencimento { get; set; }


        [Required]
        public string CodSeguranca { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
    }
}
