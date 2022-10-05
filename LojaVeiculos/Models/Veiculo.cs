namespace LojaVeiculos.Models
{
    public class Veiculo
    {
        //PrimaryKey
        public int Id { get; set; }

        public string Placa { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public decimal Km { get; set; }
        public decimal Valor { get; set; }

        public TipodeCombustivel tipo { get; set; }

        //Foreing Key´s
        public int IdConcessionaria { get; set; }
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
