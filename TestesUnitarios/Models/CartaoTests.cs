using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class CartaoTests
    {
        [Fact]
        public void TestRetornarCartaoNotNull()
        {
            //Preparação
            Cartao cartao;

            //Execução
            cartao = new Cartao();

            // Retorno esperado
            Assert.NotNull(cartao);
        }
    }
}
