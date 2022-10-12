using LojaVeiculos.Controllers;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.Mvc;
using Mock;
using System.Collections.Generic;
using Xunit;

namespace TestesUnitarios.Controllers
{
    public class TipoUsuariosControllerTests
    {
        // Preparação
        private readonly Mock<ITipoUsuarioRepository> mockRepo;
        private readonly TipoUsuarioController control;
        public TipoUsuariosControllerTests()
        {
            mockRepo = new Mock<ITipoUsuarioRepository>();
            control = new TipoUsuarioController(mockRepo.Object);
        }

        [Fact]
        public void TestActionResultReturnOk()
        {

            // Execução
            var result = control.Listar();
            // Retorno
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void TestGetAll()
        {
            // Execução 
            var Result = control.Listar();
            var ResultOk = Result as OkObjectResult;
            ResultOk.Value = new List<TipoUsuario>();
            // Retorno
            Assert.IsAssignableFrom<List<TipoUsuario>>(ResultOk.Value);

        }

        [Fact]
        public void TestStatusCodeSuccess()
        {
            // Execução 
            var Result = control.Listar();
            var result = Result as OkObjectResult;
            // Retorno
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void TestActionResultNotNull()
        {
            // Execução - Act
            var Result = control.Listar();
            // Retorno
            Assert.NotNull(Result);
        }
    }
}
