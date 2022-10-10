using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class MarcaTests
    {
        [Fact]
        public void TestRetornarMarcaNotNull()
        {
            //Preparação
            Marca marca;

            //Execução
            marca = new Marca();

            //Retorno esperado
            Assert.NotNull(marca);

            Assert.IsType<Marca>(marca); 
        }

        [Fact]
        public void TestValidarTipoMarca()
        {
            //Preparação
            Marca marca;

            //Execução
            marca = new Marca();

            //Retorno esperado
            Assert.IsType<Marca>(marca);
        }
    }
}
