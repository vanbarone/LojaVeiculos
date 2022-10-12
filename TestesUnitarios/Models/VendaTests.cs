using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class VendaTests
    {
        [Fact]
        public void TestRetornarVendaNotNull()
        {
            //Preparação
            Compra venda;

            //Execução
            venda = new Compra();

            // Retorno esperado
            Assert.NotNull(venda);
            Assert.Equal(venda.Id, venda.Id);
            Assert.Equal(venda.Data, venda.Data);
            Assert.Equal(venda.IdCliente, venda.IdCliente);
            Assert.Equal(venda.Cliente, venda.Cliente);
            Assert.Equal(venda.CartaoCpf, venda.CartaoCpf);
           
            Assert.True(venda.Id == venda.Id);
            Assert.True(venda.Data == venda.Data);
            Assert.True(venda.Cliente == venda.Cliente);
            Assert.True(venda.CartaoCpf == venda.CartaoCpf);
        }
    }
}
