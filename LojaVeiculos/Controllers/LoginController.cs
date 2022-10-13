using LojaVeiculos.Interfaces;
using LojaVeiculos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LojaVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        ILoginRepository repo;

        public LoginController(ILoginRepository _repository)
        {
            repo = _repository;
        }

        /// <summary>
        /// Realiza o login do usuário
        /// </summary>
        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            var logar = repo.Logar(email, senha);

            if (logar == null)
                return Unauthorized("Você não tem permissão para fazer login");

            return Ok(new { token = logar });
        }


        /// <summary>
        /// Gera código e envia email para redefinição da senha
        /// </summary>
        [HttpGet("RecuperarSenha/{email}")]
        public async Task<IActionResult> RecuperarSenha(string email)
        {
            if (email == "" || email == null)
                return BadRequest("Email não informado");

            var retorno = repo.RecuperarSenha(email);

            await retorno;
            
            if (retorno.IsCompletedSuccessfully)
                return Ok("Email enviado com sucesso");
            else
                return BadRequest(retorno.Status.ToString());
        }


        /// <summary>
        /// Efetua a redefinição da senha
        /// </summary>
        [HttpPut("RedefinirSenha")]
        public IActionResult RedefinirSenha(string email, string codigo, string novaSenha)
        {
            if (email == "" || email == null)
                return BadRequest("Email não informado");

            if (codigo == "" || codigo == null)
                return BadRequest("Código não informado");

            if (novaSenha == "" || novaSenha == null)
                return BadRequest("Nova senha não informada");


            try
            {
                repo.RedefinirSenha(email, codigo, novaSenha);

                return Ok("Senha alterada com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Falha na transação", Message = ex.Message });
            }
        }
    }
}
