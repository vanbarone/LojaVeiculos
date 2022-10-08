﻿using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LojaVeiculos.Controllers
{
    [Route("api/vendas")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        IRepository<Venda> repo;

        public VendaController(IRepository<Venda> _repository)
        {
            repo = _repository;
        }


        /// <summary>
        /// Lista todas as vendas cadastrados
        /// </summary>
        /// <returns>Lista de objetos(Vendas),
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
        /// Mostra a venda cadastrada com o id informado
        /// </summary>
        /// <param name="id">Id da venda</param>
        /// <returns>Objeto(Venda) se a venda foi encontrada, 
        ///          NOT FOUND se a venda não foi encontrada,
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
        /// Cadastra uma nova venda
        /// </summary>
        /// <param name="entity">Objeto(Venda) com todos os dados da venda</param>
        /// <returns>Objeto(Venda) se a inclusão foi realizada com sucesso, 
        ///          Erro 500 se deu falha na transação</returns>
        [HttpPost]
        public IActionResult Insert(Venda entity)
        {
            try
            {
                if (entity.ItensVenda.Count == 0)
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