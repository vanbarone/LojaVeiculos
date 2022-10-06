namespace LojaVeiculos.Models
{
    public class Modelo
    {
        //PrimaryKey
        public int Id { get; set; }
        public string NomeModelo { get; set; }
        public string Categoria { get; set; }

        //Foreign Key
        public int IdMarca { get; set; }

    }
}
