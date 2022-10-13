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
    [ApiController]
    public class AdministradoresController : ControllerBase
    {
        private readonly IRepository<Usuario> repo;

        public AdministradoresController(IRepository<Usuario> _repository)
        {
            repo = _repository;
        }

        /// <summary>
        /// Cadastra um administrador
        /// </summary>
        /// <param name="entity">Dados do administrador</param>
        /// <returns>Objeto(administrador),
        ///          Erro 500 se deu falha na transação</returns>
        [HttpPost]
        public IActionResult Cadastrar(Usuario entity)
        {
            try
            {
                Usuario retorno = repo.Insert(entity);

                // Retorna o administrador que foi inserido
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
        /// Lista todos os administradores cadastrados
        /// </summary>
        /// <returns>Lista de objetos(administrador),
        ///          Erro 500 se deu falha na transação</returns>
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
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
        /// Mostra o administrador cadastrado com o Id informado
        /// </summary>
        /// <returns>Objeto(Administrador) se o administrador foi encontrado, 
        ///          NOT FOUND se o administrador não foi encontrado,
        ///          Erro 500 se deu falha na transação</returns>
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
                        Message = "Administrador não encontrado"
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
        /// Alterar todos os dados de um administrador
        /// </summary>
        /// <param name="entity">Objeto(Administrador) com todos os dados do administrador</param>
        /// <param name="id">Id do administrador</param>
        /// <returns>Mensagem("Registro alterado com sucesso" se a alteração foi realizada com sucesso, 
        ///          BAD REQUEST se o id não foi informado no corpo do objeto ou se o id informado e o id do administrador forem diferentes,
        ///          NOT FOUND se o administrador não foi encontrado,
        ///          Erro 500 se deu falha na transação</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public IActionResult Alterar(int id, Usuario entity)
        {
            try
            {
                //Verifica se o id foi informado no corpo do objeto
                if (entity.Id == 0)
                    return BadRequest("Informe o campo 'id' no corpo do objeto (ex.: 'id': 1)");

                // Verifica se os ids existem
                if (id != entity.Id)
                    return BadRequest(new { message = "Dados não conferem (id da entidade é diferente do id informado)" });

                //Verifica se existe registro com o id informado
                if (repo.FindById(id) == null)
                    return NotFound(new { message = "Não existe registro cadastrado com esse 'id'" });

                //criptografa a senha
                entity.Senha = BCrypt.Net.BCrypt.HashPassword(entity.Senha);

                //Efetua a alteração
                repo.Update(entity);

                return Ok(new { Msg = "Registro alterado com sucesso" });
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
        /// Altera parcialmente os dados de um administrador
        /// </summary>
        /// <param name="id">Id do administrador</param>
        /// <param name="patch">Objeto com os dados do administrador que serão alterados</param>
        /// <returns>Mensagem("Dados alterados com sucesso" se a alteração foi realizada com sucesso, 
        ///          BAD REQUEST se o id não foi informado no corpo do objeto ou se o id informado e o id do administrador forem diferentes,
        ///          NOT FOUND se o administrador não foi encontrado,
        ///          Erro 500 se deu falha na transação</returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchUsuario)
        {
            if (patchUsuario == null)
                return BadRequest(new { message = "Não foi informado o objeto com as alterações desejadas" });

            //verifica se existe o registro no banco de dados
            var usuario = repo.FindById(id);

            if (usuario == null)
                return NotFound(new { message = "Não existe registro cadastrado com esse 'id'" });

            // Pega o patch e o usuário encontrado
            repo.UpdatePartial(patchUsuario, usuario);

            return Ok(usuario);
        }


        /// <summary>
        /// Exclui um administrador
        /// </summary>
        /// <param name="id">Id do administrador</param>
        /// <returns>Mensagem("Registro excluído com sucesso" se a exclusão foi realizada com sucesso, 
        ///          NOT FOUND se o administrador não foi encontrado,
        ///          Erro 500 se deu falha na transação</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMINISTRADOR")]
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
                        Message = "Administrador não encontrado"
                    });
                }

                // Exclui por busca de id
                repo.Delete(busca);

                return Ok(new
                {
                    msg = "Registro excluído com sucesso!"
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
