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
        [StringLength(60)]
        public string Nome { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Endereco { get; set; }

        [Required]
        [StringLength(60)] 
        public string Bairro  { get; set; }

        [Required]
        [StringLength(60)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        [StringLength(9)]
        public string Cep { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(255)]
        public string Site { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public ICollection<Veiculo> Veiculos { get; set; }
    }
}
