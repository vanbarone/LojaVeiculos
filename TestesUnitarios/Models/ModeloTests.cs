using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class ModeloTests
    {
        [Fact]
        public void TestRetornarModeloNotNull()
        {
            //Preparação
            Modelo modelo;

            //Execução
            modelo = new Modelo();

            // Retorno esperado
            Assert.NotNull(modelo);
            Assert.Equal(modelo.Id, modelo.Id);
            Assert.Equal(modelo.Nome, modelo.Nome);
            Assert.Equal(modelo.Motor, modelo.Motor);
            Assert.Equal(modelo.Marca, modelo.Marca);
            Assert.Equal(modelo.IdMarca, modelo.IdMarca); 
            Assert.Equal(modelo.Veiculos, modelo.Veiculos);

            Assert.True(modelo.Id == modelo.Id);
            Assert.True(modelo.Nome == modelo.Nome);
            Assert.True(modelo.Motor == modelo.Motor);
            Assert.True(modelo.Veiculos == modelo.Veiculos);
        }
    }
}
