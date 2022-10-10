using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class TipoUsuarioTests
    {
        [Fact]
        public void TestRetornarTipoUsuarioNotNull()
        {
            //Preparação
            TipoUsuario tipoUsuario;

            //Execução
            tipoUsuario = new TipoUsuario();

            //Retorno esperado
            Assert.NotNull(tipoUsuario);
        }
    }
}
