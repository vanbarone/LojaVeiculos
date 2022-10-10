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
            Assert.Equal(modelo.Categoria, modelo.Categoria);
            Assert.Equal(modelo.NomeModelo, modelo.NomeModelo);
            Assert.Equal(modelo.Marca, modelo.Marca);
            Assert.Equal(modelo.IdMarca, modelo.IdMarca); 
            Assert.Equal(modelo.Veiculos, modelo.Veiculos);

            Assert.True(modelo.Id == modelo.Id);
            Assert.True(modelo.Categoria == modelo.Categoria);
            Assert.True(modelo.NomeModelo == modelo.NomeModelo);
            Assert.True(modelo.Veiculos == modelo.Veiculos);
        }
    }
}
