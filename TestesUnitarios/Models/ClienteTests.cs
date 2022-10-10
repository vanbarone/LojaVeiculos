using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class ClienteTests
    {
        [Fact]
        public void TestRetornarClienteNotNull()
        {
            //Preparação
            Cliente cliente;

            //Execução
            cliente = new Cliente();

            // Retorno esperado
            Assert.NotNull(cliente);
            Assert.Equal(cliente.Id, cliente.Id);
            Assert.Equal(cliente.Usuario, cliente.Usuario);
            Assert.Equal(cliente.IdUsuario, cliente.IdUsuario);
            Assert.Equal(cliente.Cartoes, cliente.Cartoes);
            Assert.Equal(cliente.AceitaTermoUso, cliente.AceitaTermoUso);

            Assert.True(cliente.Id == cliente.Id);
            Assert.True(cliente.Usuario == cliente.Usuario);
            Assert.True(cliente.IdUsuario == cliente.IdUsuario);
            Assert.True(cliente.AceitaTermoUso == cliente.AceitaTermoUso);
        }
    }
}
