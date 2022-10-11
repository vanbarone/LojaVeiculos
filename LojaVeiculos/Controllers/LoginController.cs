using LojaVeiculos.Interfaces;
using LojaVeiculos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        /// Logar usuario na aplicação
        /// </summary>
        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            var logar = repo.Logar(email, senha);

            if (logar == null)
                return Unauthorized("você não tem permissão para acesar esse recurso");

            return Ok(new { token = logar });
        }


        /// <summary>
        /// Envia email para recuperação de senha
        /// </summary>
        [HttpGet]
        public IActionResult RecuperarSenha(string email)
        {
            Email.Execute().Wait();


            return Ok("Email enviado com sucesso");
        }
    }
}
