using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "ADMINISTRADOR")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        IVeiculoRepository repo;

        public VeiculosController(IVeiculoRepository _repository)
        {
            repo = _repository;
        }


        /// <summary>
        /// Lista todos os veículos cadastrados
        /// </summary>
        /// <returns>Lista de objetos(Veiculos),
        ///          Erro 500 se deu falha na transação</returns>
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


        /// <summary>
        /// Mostra o veículo cadastrado com o id informado
        /// </summary>
        /// <param name="id">Id do veículo</param>
        /// <returns>Objeto(Veiculo) se o veículo foi encontrado, 
        ///          NOT FOUND se o veículo não foi encontrado,
        ///          Erro 500 se deu falha na transação</returns>
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


        /// <summary>
        /// Mostra o veículo cadastrado com a placa informada
        /// </summary>
        /// <param name="placa">Placa do veículo</param>
        /// <returns>Objeto(Veiculo) se o veículo foi encontrado, 
        ///          NOT FOUND se o veículo não foi encontrado,
        ///          Erro 500 se deu falha na transação</returns>
        [HttpGet("Buscar/{placa}")]
        public IActionResult GetByPlaca(string placa)
        {
            try
            {
                var obj = repo.FindByPlaca(placa);

                if (obj == null)
                    return NotFound(new { Error = "Não existe registro cadastrado com essa 'placa'" });

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }


        /// <summary>
        /// Cadastra um novo veículo
        /// </summary>
        /// <param name="entity">Objeto(Veiculo) com todos os dados do veiculo</param>
        /// <returns>Objeto(Veiculo) se a inclusão foi realizada com sucesso, 
        ///          Erro 500 se deu falha na transação</returns>
        [HttpPost]
        public IActionResult Insert(Veiculo entity)
        {
            try
            {
                if (entity.IdModelo == 0)
                    return BadRequest(new { Error = "Informe o Id do Modelo" });

                var obj = repo.Insert(entity);

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        /// <summary>
        /// Altera todos os dados de um veiculo
        /// </summary>
        /// <param name="id">Id do veículo</param>
        /// <param name="entity">Objeto(Veiculo) com todos os dados do veiculo</param>
        /// <returns>Mensagem("Registro alterado com sucesso" se a alteração foi realizada com sucesso, 
        ///          BAD REQUEST se o id informado e o id do veículo forem diferentes,
        ///          NOT FOUND se o veículo não foi encontrado,
        ///          Erro 500 se deu falha na transação</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, Veiculo entity)
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

                //Efetua a alteração
                repo.Update(entity);

                return Ok(new { Msg = "Registro alterado com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        /// <summary>
        /// Altera alguns dados de um veiculo
        /// </summary>
        /// <param name="id">Id do veículo</param>
        /// <param name="patch">Objeto com os dados do veiculo que serão alterados</param>
        /// <returns>Mensagem("Dados alterados com sucesso" se a alteração foi realizada com sucesso, 
        ///          BAD REQUEST se o id informado e o id do veículo forem diferentes,
        ///          NOT FOUND se o veículo não foi encontrado,
        ///          Erro 500 se deu falha na transação</returns>
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

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }


        /// <summary>
        /// Exclui um veículo
        /// </summary>
        /// <param name="id">Id do veículo</param>
        /// <returns>Mensagem("Registro excluído com sucesso" se a exclusão foi realizada com sucesso, 
        ///          NOT FOUND se o veículo não foi encontrado,
        ///          Erro 500 se deu falha na transação</returns>
        [HttpDelete("{id}")]
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
