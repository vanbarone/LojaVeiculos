using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LojaVeiculos.Models;
using LojaVeiculos.Interfaces;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMINISTRADOR")]
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
                //Verifica se o id foi informado no corpo do objeto
                if (concessionaria.Id == null || concessionaria.Id == 0)
                    return BadRequest("Informe o campo 'id' no corpo do objeto (ex.: 'id': 1)");

                // Verifica se os ids existem
                if (id != concessionaria.Id)
                    return BadRequest(new { message = "Dados não conferem (id da entidade é diferente do id informado)" });

                //Verifica se existe registro com o id informado
                if (repo.FindById(id) == null)
                    return NotFound(new { message = "Não existe registro cadastrado com esse 'id'" });

                //Efetua a alteração
                repo.Update(concessionaria);

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


        [HttpPatch("{id}")]
        public IActionResult AlterarParcial(int id, [FromBody] JsonPatchDocument patch)
        {
            if (patch == null)
                return BadRequest(new { message = "Não foi informado o objeto com as alterações desejadas" });

            //verifica se existe o registro no banco de dados
            var concessionaria = repo.FindById(id);

            if (concessionaria == null)
                return NotFound(new { message = "Não existe registro cadastrado com esse 'id'" });

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

                return Ok(new { Msg = "Registro excluído com sucesso" });
            }
            catch (System.Exception ex )
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
