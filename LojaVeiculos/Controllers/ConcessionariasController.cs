using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LojaVeiculos.Models;
using LojaVeiculos.Interfaces;
using System.Text.Json;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcessionariasController : ControllerBase
    {
        IRepository<Concessionaria> repo;

        public ConcessionariasController(IRepository<Concessionaria> _repository)
        {
            repo = _repository;
        }

        /// <summary>
        /// Listar todas as concessionárias.
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
        /// Cadastrar uma concessionária
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Cadastrar(Concessionaria entity)
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
        /// Buscar por Id a Concessionária
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
                { return NotFound(new { Message = "Concessionaria não encontrada" }); }

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
        /// Alterar a concessionária
        /// </summary>
        /// <param name="id"></param>
        /// <param name="concessionaria"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Alterar(int id, Concessionaria concessionaria)
        {
            try
            {
                if (id != concessionaria.Id)
                    return BadRequest();

                repo.Update(concessionaria);
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


        [HttpPatch("{id}")]
        public IActionResult AlterarParcial(int id, [FromBody] JsonDocument patch)
        {
            if (patch ==null)
            {
                return BadRequest();
            }

            var concessionaria = repo.FindById(id);
            if( concessionaria==null)
            { return NotFound(new { Message = "Concessionária não encontrada" }); }

            repo.UpdatePartial(patch, concessionaria);
            return Ok(concessionaria);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var busca = repo.FindById(id);

                if (busca==null)
                { return NotFound(new { Message = "Concessionária não encontrada" }); }

                repo.Delete(busca);
                return NoContent();
            }
            catch (System.Exception ex )
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
