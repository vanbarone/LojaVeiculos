using LojaVeiculos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMINISTRADOR")]
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
