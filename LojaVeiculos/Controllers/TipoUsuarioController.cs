using LojaVeiculos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "ADMINISTRADOR")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        ITipoUsuarioRepository repo;

        public TipoUsuarioController(ITipoUsuarioRepository _repository)
        {
            repo = _repository;
        }
        /// <summary>
        /// Lista todos os tipos de usuários cadastrados
        /// </summary>
        /// <returns>Lista os tipos de usuários e retorna

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repo.ListarTodos();

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message
                });
            }
        }

    }
}
