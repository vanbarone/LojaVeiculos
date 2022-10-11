using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartoesController : ControllerBase
    {
        private readonly IRepository<Cartao> repo;

        //metodo construtor pegando repositorio
        public CartoesController(IRepository<Cartao> _repositorio)
        {
            repo = _repositorio;
        }


        /// <summary>
        /// Listando cartoes
        /// </summary>
        /// <returns>Cartao listada</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                //retornando Listar do repositorio
                return Ok(repo.FindAll());
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    ex.Message,
                });
            }
        }
    }
}
