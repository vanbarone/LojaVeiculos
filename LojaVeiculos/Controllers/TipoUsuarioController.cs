using LojaVeiculos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        ITipoUsuarioRepositorie repo;

        public TipoUsuarioController(ITipoUsuarioRepositorie _repository)
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

        /// <summary>
        /// Lista o tipo usuário por id
        /// </summary>
        /// <param name="id">tipo do usuário</param>
        /// <returns>Retorna o tipo de usuário, se não encontrar da erro</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorID(int id)
        {
            try
            {
                var retorno = repo.BuscarPorId(id);
                if(retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Tipo de usuário não encontrado"
                    });
                }
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
