using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        IRepository<Veiculo> repo;

        public VeiculoController(IRepository<Veiculo> _repository)
        {
            repo = _repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var lista = repo.FindAll();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { Error = "Não existe registro cadastrado com esse 'id'" });

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }


        [HttpPost]
        public IActionResult Insert(Veiculo entity)
        {
            try
            {
                var obj = repo.Insert(entity);

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, Veiculo entity)
        {
            try
            {
                //verifica se o id informado é diferente do id da entidade
                if (id != entity.Id)
                    return BadRequest(new { message = "Dados não conferem (id da entidade é diferente do id informado)" });

                //verifica se existe o registro no banco de dados
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { message = "Não existe registro cadastrado com esse 'id'" });

                //Efetua a alteração
                repo.Update(entity);

                return Ok(new { Msg = "Registro alterado com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, [FromBody] JsonPatchDocument patch)
        {
            try
            {
                if (patch == null)
                    return BadRequest(new { message = "Não foi informado o objeto com as alterações desejadas" });

                //verifica se existe o registro no banco de dados
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { message = "Não existe registro cadastrado com esse 'id'" });

                //Efetua a alteração
                repo.UpdatePartial(patch, obj);

                return Ok(new { Msg = "Dados alterados com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        [HttpDelete("{Id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                //verifica se existe o registro no banco de dados
                var obj = repo.FindById(id);

                if (obj == null)
                    return NotFound(new { message = "Não existe registro cadastrado com esse 'id'" });

                //Efetua a exclusão
                repo.Delete(obj);

                return Ok(new { Msg = "Registro excluído com sucesso" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }
    }
}
