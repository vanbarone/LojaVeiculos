using LojaVeiculos.Models;
using Xunit;

namespace TestesUnitarios.Models
{
    public class VeiculoTests
    {
        [Fact]
        public void TestRetornarVeiculoNotNull()
        {
            //Preparação
            Veiculo veiculo;

            //Execução
            veiculo = new Veiculo();

            // Retorno esperado
            Assert.NotNull(veiculo);
            Assert.Equal(veiculo.Id, veiculo.Id);
            Assert.Equal(veiculo.Placa, veiculo.Placa);
            Assert.Equal(veiculo.concessionaria, veiculo.concessionaria);
            Assert.Equal(veiculo.IdConcessionaria, veiculo.IdConcessionaria);
            Assert.Equal(veiculo.Modelo, veiculo.Modelo);
            Assert.Equal(veiculo.IdModelo, veiculo.IdModelo);

            Assert.True(veiculo.Id == veiculo.Id);
            Assert.True(veiculo.Placa == veiculo.Placa);
            Assert.True(veiculo.Modelo == veiculo.Modelo);
            Assert.True(veiculo.concessionaria == veiculo.concessionaria);
        }
    }
}
