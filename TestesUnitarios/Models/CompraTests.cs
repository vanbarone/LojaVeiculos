using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class CompraTests
    {
        [Fact]
        public void TestRetornarVendaNotNull()
        {
            //Preparação
            Compra compra;

            //Execução
            compra = new Compra();

            // Retorno esperado
            Assert.NotNull(compra);
            Assert.Equal(compra.Id, compra.Id);
            Assert.Equal(compra.Data, compra.Data);
            Assert.Equal(compra.IdCliente, compra.IdCliente);
            Assert.Equal(compra.Cliente, compra.Cliente);
            Assert.Equal(compra.CartaoCpf, compra.CartaoCpf);
           
            Assert.True(compra.Id == compra.Id);
            Assert.True(compra.Data == compra.Data);
            Assert.True(compra.Cliente == compra.Cliente);
            Assert.True(compra.CartaoCpf == compra.CartaoCpf);
        }
    }
}
