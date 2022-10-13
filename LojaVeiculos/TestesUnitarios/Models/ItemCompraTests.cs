using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class ItemCompraTests
    {
        [Fact]
        public void TestRetornarItemCompraNotNull()
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
