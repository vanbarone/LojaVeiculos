namespace LojaVeiculos.Models
{
    public class Concessionaria
    {
        //Primary Key
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereço { get; set; }
        public string Bairro  { get; set; }

        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Telefone { get; set; }
        public string Site { get; set; }

        //Foreing Key
        public int IdVeiculo { get; set; }
    }
}
