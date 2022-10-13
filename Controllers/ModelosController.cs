using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMINISTRADOR")]
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
        public IActionResult Alterar(int id, Modelo entity)
        {
            try
            {
                //Verifica se o id foi informado no corpo do objeto
                if (entity.Id == null || entity.Id == 0)
                    return BadRequest("Informe o campo 'id' no corpo do objeto (ex.: 'id': 1)");

                //verifica se o id informado é diferente do id da entidade
                if (id != entity.Id)
                    return BadRequest(new { message = "Dados não conferem (id da entidade é diferente do id informado)" });

                //Verifica se existe registro com o id informado
                if (repo.FindById(id) == null)
                    return NotFound(new { message = "Não existe registro cadastrado com esse 'id'" });


                //
                repo.Update(entity);

                return Ok(new { Msg = "Registro alterado com sucesso" });
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

                return Ok(new { Msg = "Registro excluído com sucesso" });
            }
            catch (System.Exception ex)
            {

                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message,
                    Inner = ex.InnerException?.Message
                });
            }
        }
    }
}

