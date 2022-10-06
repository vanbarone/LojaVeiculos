using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVeiculos.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        public string Placa { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public decimal Km { get; set; }
        public decimal Valor { get; set; }

        public TipodeCombustivel tipo { get; set; }

        [ForeignKey("Concessionaria")]
        public int IdConcessionaria { get; set; }

        [ForeignKey("Modelo")]
        public int IdModelo { get; set; }

        public Status status { get; set; }

        public enum Status
        {
            Em Estoque,
            Vendido
        }
        public enum TipodeCombustivel
        {
            Gasolina,
            Etanol,
            Flex,
            S10
        }
    }
}
