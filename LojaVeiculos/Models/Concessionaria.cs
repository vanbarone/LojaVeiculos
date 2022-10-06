using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVeiculos.Models
{
    public class Concessionaria
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Endereço { get; set; }

        [Required] 
        public string Bairro  { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string CEP { get; set; }

        public string Telefone { get; set; }

        [Required]
        public string Site { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public ICollection<Veiculo> Veiculos;
    }
}
