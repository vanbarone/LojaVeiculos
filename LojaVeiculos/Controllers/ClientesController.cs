using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IRepository<Cliente> repo;

        //metodo construtor pegando repositorio
        public ClientesController(IRepository<Cliente> _repositorio)
        {
            repo = _repositorio;
        }

        /// <summary>
        /// Cadastrar um cliente
        /// </summary>
        /// <param name="cliente">Guardar dados do cliente</param>
        /// <returns>Cliente cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(Cliente cliente)
        {
            try
            {
                //retornando Cadastrar do repositorio
                return Ok(repo.Insert(cliente));
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    ex.Message,
                });
            }
        }

        /// <summary>
        /// Listando clientes
        /// </summary>
        /// <returns>Cliente listado</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                //retornando Listar do repositorio
                return Ok(repo.FindAll());
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    ex.Message,
                });
            }
        }

        /// <summary>
        /// Listar um cliente por id
        /// </summary>
        /// <param name="id">Pegar cliente por id</param>
        /// <returns>Cliente selecionado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarId(int id)
        {
            try
            {
                //chamando repositorio
                var retorno = repo.FindById(id);
                //validar para ver se o id inserido existe no banco
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Cliente não encontrado"
                    });
                }
                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    ex.Message,
                }); ;
            }
        }

        /// <summary>
        /// Para alterar um cliente
        /// </summary>
        /// <param name="id">Pegar cliente por id</param>
        /// <param name="cliente">Guardar dados atualizados</param>
        /// <returns>Cliente alterado</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Cliente cliente)
        {
            try
            {
                //validando para ver se o id inserido eh de algum cliente
                if (id != cliente.Id)
                {
                    return BadRequest("Nao existe o id inserido");
                }
                //chamando repositorio
                var retorno = repo.FindById(id);
                //validando retorno
                if (retorno == null)
                {
                    return NotFound(new
                    {
                        Message = "Cliente nao encontrado"
                    });
                }
                return NoContent();
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    ex.Message,
                }); ;
            }
        }

        /// <summary>
        /// Alterar parcialmente o cliente
        /// </summary>
        /// <param name="id">Pegar cliente por id</param>
        /// <param name="patchCliente">Guardar cliente atualizado</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument patchCliente)
        {
            try
            {
                //validando para ver se existe no banco de dados
                if (patchCliente == null)
                {
                    return BadRequest();
                }
                //chamando repositorio
                var cliente = repo.FindById(id);
                //validando cliente
                if (cliente == null)
                {
                    return NotFound(new
                    {
                        Message = "Cliente nao encontrado"
                    });
                }
                //alterando
                repo.UpdatePartial(patchCliente, cliente);
                return Ok(cliente);
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    ex.Message,
                }); ;
            }
        }

        /// <summary>
        /// Deletar um cliente
        /// </summary>
        /// <param name="id">Pegar cliente por id</param>
        /// <returns>Cliente deletado</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //chamando repositorio
                var busca = repo.FindById(id);
                //validando a busca do cliente
                if (busca == null)
                {
                    return NotFound(new
                    {
                        Message = "Cliente nao encontrado"
                    });
                }

                repo.Delete(busca);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                //retornado mensagem de erro
                return StatusCode(500, new
                {
                    Error = "Falha na conexao",
                    ex.Message,
                });
            }
        }
    }
}
