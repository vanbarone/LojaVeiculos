using LojaVeiculos.Interfaces;
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
            {
                return Unauthorized();
            }
            return Ok(new { token = logar });
        }
    }
}
