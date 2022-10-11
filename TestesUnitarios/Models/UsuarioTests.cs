using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class UsuarioTests
    {
        [Fact]
        public void TestRetornarUsuarioNotNull()
        {
            // Preparação
            Usuario usuario;

            // Execução
            usuario = new Usuario();

            // Retorno esperado
            Assert.NotNull(usuario);

            Assert.IsType<Usuario>(usuario);
            Assert.Equal(usuario.Id, usuario.Id);
            Assert.Equal(usuario.Nome, usuario.Nome);
            Assert.Equal(usuario.Email, usuario.Email);
            Assert.Equal(usuario.Senha, usuario.Senha);
            Assert.Equal(usuario.TipoUsuario, usuario.TipoUsuario);
           
            Assert.True(usuario.Id == usuario.Id);
            Assert.True(usuario.Nome == usuario.Nome);
            Assert.True(usuario.Senha == usuario.Senha);
            Assert.True(usuario.TipoUsuario == usuario.TipoUsuario);
        }
    }
}
