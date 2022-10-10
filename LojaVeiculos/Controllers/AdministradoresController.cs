using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using LojaVeiculos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "ADMINISTRADOR")]
    [ApiController]
    public class AdministradoresController : ControllerBase
    {
        private readonly IRepository<Usuario> repo;

        public AdministradoresController(IRepository<Usuario> _repository)
        {
            repo = _repository;
        }

        /// <summary>
        /// Cadastra usuario
        /// </summary>
        /// <param name="entity">Dados do usuario</param>
        /// <returns>Dados do usuario cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Usuario entity)
        {
            try
            {
                Usuario retorno = repo.Insert(entity);

                // Retorna o usuario que foi inserido
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new 
                { 
                    Error = "Falha na transação", 
                    Message = ex.Message, 
                    Inner = ex.InnerException?.Message 
                });
            }
        }

        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                // Cria a variável retorno para receber o método de listar
                var retorno = repo.FindAll();

                // Retorna a variável
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                // Se não for inserida da erro
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message,
                });
            }
        }
        /// <summary>
        /// Busca todos os suários cadastradas por id
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarUsuarioPorID(int id)
        {
            try
            {
                var retorno = repo.FindById(id);

                // Se o id for nulo
                if (retorno == null)
                {
                    // Retorna erro informando que não foi encontrado
                    return NotFound(new
                    {
                        Message = "Usuário não encontrado"
                    });
                }
                // Retorna o usuário por id
                return Ok(retorno);

            }
            catch (System.Exception ex)
            {
                // Se não for inserida da erro
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message,
                });
            }
        }
        /// <summary>
        /// Alterar os dados do usuário
        /// </summary>
        /// <param name="entity">Todas as informações do usuário</param>
        /// <param name="id">Id do usuário</param>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Usuario entity)
        {
            try
            {
                // Verifica se os ids existem
                if (id != entity.Id)
                {
                    // Retorna erro
                    return BadRequest("Os ids são diferentes");
                }

                // Verifica se o id existe no banco
                var retorno = repo.FindById(id);

                // Se o id for nulo
                if (retorno == null)
                {
                    // Retorna erro informando que não foi encontrado
                    return NotFound(new
                    {
                        Message = "Usuário não encontrado"
                    });
                }

                //criptografa a senha
                entity.Senha = BCrypt.Net.BCrypt.HashPassword(entity.Senha);

                // Efetiva a alteração
                repo.Update(entity);

                // Retorna sucesso, não retorna o objeto
                return Ok(new 
                { 
                    Message = "Registro alterado com sucesso" 
                });
            }
            catch (System.Exception ex)
            {

                // Se não for inserida da erro
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message,
                });
            }
        }
        /// <summary>
        /// Altera os dados parcialmente
        /// </summary>
        /// <returns>Dados alterados</returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchUsuario)
        {
            // Verifica se o Patch está vazio
            if (patchUsuario == null)
            {
                // Retorna erro
                return BadRequest();
            }

            // Busca o objeto
            var usuario = repo.FindById(id);
            // Se o id for nulo
            if (usuario == null)
            {
                // Retorna erro informando que não foi encontrado
                return NotFound(new
                {
                    Message = "Usuário não encontrado"
                });
            }

            // Pega o patch e o usuário encontrado
            repo.UpdatePartial(patchUsuario, usuario);
            return Ok(usuario);
        }
        /// <summary>
        /// Deleta todos dados de um usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Mensagem de exclusão</returns>

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                // Busca por id
                var busca = repo.FindById(id);
                // Se busca nulo
                if (busca == null)
                {
                    // Retorna erro informando que não foi encontrado
                    return NotFound(new
                    {
                        Message = "Usuário não encontrado"
                    });
                }
                // Exclui por busca de id
                repo.Delete(busca);

                return Ok(new
                {
                    msg = "Usuário exlcuído com sucesso!"
                });

            }
            catch (System.Exception ex)
            {
                // Se não for inserida da erro
                return StatusCode(500, new
                {
                    Error = "Falha na transação",
                    Message = ex.Message,
            });
            }
        }
    }
}
