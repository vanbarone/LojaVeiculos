﻿using Microsoft.AspNetCore.Http;
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
        public IActionResult Alterar(int id, Marca marca)
        {
            try
            { // verifica se o id informado é diferente do id da entidade
                if (id != marca.Id)
                    return BadRequest(new {Message = "Dados não conferem (id da entidade é diferente do Id informado)"});

                //verifica se existe o registro
                var obj = repo.FindById(id);
                if (obj == null)
                    return NotFound(new { Message = "Não existe registro cadastrado com esse id" });

                repo.Update(marca);
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

