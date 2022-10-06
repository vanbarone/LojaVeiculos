using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LojaVeiculos.Models
{
    public class Usuario
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Nome { get; set; }

        [Required]
        [StringLength(60)]
        public string Sobrenome { get; set; }

        [Required,StringLength(11)]
        public string CPF { get; set; }

        public string Celular { get; set; }

        [Required(ErrorMessage = "Informe seu email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [MinLength(4)]
        public string Senha { get; set; }

        [ForeignKey("TipoUsuario")]
        public int? IdTipoUsuario { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public TipoUsuario TipoUsuario { get; set; }
    }
}