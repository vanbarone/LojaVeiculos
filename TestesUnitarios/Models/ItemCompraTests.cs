using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class ItemCompraTests
    {
        [Fact]
        public void TestRetornarItemVendaNotNull()
        {
            //Preparação
            ItemCompra itemCompra;

            //Execução
            itemCompra = new ItemCompra();

            //Retorno esperado
            Assert.NotNull(itemCompra);

            Assert.IsType<ItemCompra>(itemCompra);
        }

        [Fact]
        public void TestValidarTipoItemVenda()
        {
            //Preparação
            ItemCompra itemCompra;

            //Execução
            itemCompra = new ItemCompra();

            //Retorno esperado
            Assert.IsType<ItemCompra>(itemCompra);
        }
    }
}
