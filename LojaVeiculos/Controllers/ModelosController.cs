using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelosController : ControllerBase
    {
        IRepository<Modelo> repo;

        public ModelosController(IRepository<Modelo> _repository)
        {
            repo = _repository;
        }

        /// <summary>
        /// Listar todos os modelos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var retorno = repo.FindAll();
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
        /// Cadastrar um modelo.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Cadastrar(Modelo entity)
        {
            try
            {
                var obj = repo.Insert(entity);
                return Ok(obj);

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
        /// Buscar um Modelo por Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult BuscarporId(int id)
        {
            try
            {
                var retorno = repo.FindById(id);

                if (retorno == null)
                { return NotFound(new { Message = "Modelo não encontrado" }); }

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
      /// Alterar um modelo.
      /// </summary>
      /// <param name="id"></param>
      /// <param name="modelo"></param>
      /// <returns></returns>
        [HttpPut]
        public IActionResult Alterar(int id, Modelo modelo)
        {
            try
            {
                if (id != modelo.Id)
                    return BadRequest();

                repo.Update(modelo);
                return NoContent();
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
        /// Alterar um modelo pelo patch.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult AlterarParcial(int id, [FromBody] JsonPatchDocument patch)
        {
            if (patch == null)
            {
                return BadRequest();
            }

            var modelo = repo.FindById(id);
            if (modelo == null)
            { return NotFound(new { Message = "Modelo não encontrado" }); }

            repo.UpdatePartial(patch, modelo);
            return Ok(modelo);
        }

        /// <summary>
        /// Deletar um modelo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var busca = repo.FindById(id);

                if (busca == null)
                { return NotFound(new { Message = "Modelo não encontrado" }); }

                repo.Delete(busca);
                return NoContent();
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

