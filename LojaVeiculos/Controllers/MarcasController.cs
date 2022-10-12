using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LojaVeiculos.Models;
using LojaVeiculos.Interfaces;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        IRepository<Marca> repo;
        
        public MarcasController(IRepository<Marca> _repository)
        {
            repo = _repository;
        }

        
        /// <summary>
        /// Listar todas as Marcas.
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
       /// Cadastrar uma nova Marca.
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
        [HttpPost]
        public IActionResult Cadastrar(Marca entity)
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
        /// Buscar por Id uma Marca.
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
                { return NotFound(new { Message = "Marca não encontrada" }); }

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
        /// Alterar uma marca.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="marca"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Alterar(int id, Marca entity)
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

                return Ok(new { Message = "Registro alterado com sucesso" });
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
        /// Deletar uma Marca
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
                { return NotFound(new { Message = "Marca não encontrada" }); }

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

