using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class ItemVendaTests
    {
        [Fact]
        public void TestRetornarItemVendaNotNull()
        {
            //Preparação
            ItemVenda itemVenda;

            //Execução
            itemVenda = new ItemVenda();

            //Retorno esperado
            Assert.NotNull(itemVenda);

            Assert.IsType<ItemVenda>(itemVenda);
        }

        [Fact]
        public void TestValidarTipoItemVenda()
        {
            //Preparação
            ItemVenda itemVenda;

            //Execução
            itemVenda = new ItemVenda();

            //Retorno esperado
            Assert.IsType<ItemVenda>(itemVenda);
        }
    }
}
