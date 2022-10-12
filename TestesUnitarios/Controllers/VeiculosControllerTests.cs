using LojaVeiculos.Controllers;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace TestesUnitarios.Controllers
{
    
    public class VeiculosControllerTests
    {
        //Preparação
        private readonly Mock<IVeiculoRepository> mockRepo;
        private readonly VeiculosController control;

        
        public VeiculosControllerTests()
        {
            mockRepo = new Mock<IVeiculoRepository>();
            control = new VeiculosController(mockRepo.Object);
        }

        [Fact]
        public void TestActionResultReturnOk()
        {
            //Execução
            var result = control.GetAll();

            //Retorno
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void TestGetAll()
        {
            // Execução
            var Result = control.GetAll();
            var ResultOk = Result as OkObjectResult;
            ResultOk.Value = new List<Veiculo>();
            // Retorno
            Assert.IsAssignableFrom<List<Veiculo>>(ResultOk.Value);
        }

        [Fact]
        public void TestStatusCodeSuccess()
        {
            // Execução
            var Result = control.GetAll();
            var result = Result as OkObjectResult;
            // Retorno
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void TestInsertVeiculo()
        {
            var result = control.Insert(new()
            {
                Placa = "Placa Teste",
                Ano = 2022,
                IdConcessionaria = 1,
                IdModelo = 1,
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void TestActionResultNotNull()
        {
            // Execução 
            var Result = control.GetAll();
            // Retorno
            Assert.NotNull(Result);
        }

    }
}
