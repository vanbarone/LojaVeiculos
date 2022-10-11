using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class ConcessionariaTests
    {
        [Fact]
        public void TestRetornarConcessionariaNotNull()
        {
            //Preparação
            Concessionaria concessionaria;

            //Execução
            concessionaria = new Concessionaria();

            // Retorno esperado
            Assert.NotNull(concessionaria);
            Assert.Equal(concessionaria.Id, concessionaria.Id);
            Assert.Equal(concessionaria.Nome, concessionaria.Nome);
            Assert.Equal(concessionaria.Endereco, concessionaria.Endereco);
            Assert.Equal(concessionaria.Cep, concessionaria.Cep);
            Assert.Equal(concessionaria.Site, concessionaria.Site);
            Assert.Equal(concessionaria.Cidade, concessionaria.Cidade);

            Assert.True(concessionaria.Id == concessionaria.Id);
            Assert.True(concessionaria.Nome == concessionaria.Nome);
            Assert.True(concessionaria.Endereco == concessionaria.Endereco);
            Assert.True(concessionaria.Site == concessionaria.Site);
        }
    }
}
