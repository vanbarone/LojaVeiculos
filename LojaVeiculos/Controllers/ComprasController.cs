using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "ADMINISTRADOR, CLIENTE")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        IRepository<Compra> repo;

        public ComprasController(IRepository<Compra> _repository)
        {
            repo = _repository;
        }


        /// <summary>
        /// Lista todas as compras cadastradas
        /// </summary>
        /// <returns>Lista de objetos(Compras),
        ///          Erro 500 se deu falha na transação</returns>
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
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
        /// Mostra a compra cadastrada com o id informado
        /// </summary>
        /// <param name="id">Id da compra</param>
        /// <returns>Objeto(Compra) se a compra foi encontrada, 
        ///          NOT FOUND se a compra não foi encontrada,
        ///          Erro 500 se deu falha na transação</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMINISTRADOR")]
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
        /// Cadastra uma nova compra
        /// </summary>
        /// <param name="entity">Objeto(Compra) com todos os dados da compra</param>
        /// <returns>Objeto(Compra) se a inclusão foi realizada com sucesso, 
        ///          Erro 500 se deu falha na transação</returns>
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR, CLIENTE")]
        public IActionResult Insert(Compra entity)
        {
            try
            {
                if (entity.IdCliente == 0)
                    return BadRequest(new { Error = "Informe o Id do Cliente" });

                if (entity.ItensCompra.Count == 0)
                    return BadRequest(new { Error = "Não foi informado nenhum item de venda" });

                var obj = repo.Insert(entity);

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message, Inner = ex.InnerException?.Message });
            }
        }

    }
}
