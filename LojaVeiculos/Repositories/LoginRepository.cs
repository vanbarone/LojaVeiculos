using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using LojaVeiculos.Utils;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LojaVeiculos.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        // Injeção de dependência e Metodo Construtor

        LojaVeiculosContext ctx;

        public LoginRepository(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }

        public string Logar(string email, string senha)
        {
            // Verifica se existe um usuário com email
            Usuario usuario = ctx.Usuario.Where(u => u.Email == email).FirstOrDefault();

            if (usuario != null && senha != null && usuario.Senha.Contains("$2b$"))
            {
                if (BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
                {
                    IUsuarioRepository repoUsuario = new UsuarioRepository(ctx);
                    
                    usuario = repoUsuario.FindById(usuario.Id);
                    

                    //*** Credenciais do JWT para geração do token ***

                    //definição das claims
                    var minhasClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,  usuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, usuario.TipoUsuario.Tipo.ToUpper()),
                        new Claim("ID", usuario.Id.ToString())
                    };

                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("lojaVeiculos-chave-autenticacao"));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    //geração do token
                    var meuToken = new JwtSecurityToken(
                        issuer: "lojaVeiculos.webAPI",
                        audience: "lojaVeiculos.webAPI",
                        claims: minhasClaims,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: creds
                        );

                    //*** Fim das credenciais do JWT ***

                    return new JwtSecurityTokenHandler().WriteToken(meuToken);  
                }
            }
            return null;
        }



        public async Task<string> RecuperarSenha(string email)
        {
            // Verifica se existe um usuário com email
            Usuario usuario = ctx.Usuario.Where(u => u.Email == email).FirstOrDefault();

            if (usuario == null)
                return "Usuário não cadastrado";

            //Gera código para alteração da senha
            string codigo = Guid.NewGuid().ToString().Substring(0, 10);

            //salva codigo de alteração na senha do usuário
            usuario.Senha = codigo;

            ctx.Entry(usuario).Property(s => s.Senha).IsModified = true;

            ctx.SaveChanges();


            //
            var retorno = Email.Execute(email, usuario.Nome, codigo);

            await retorno;

            return retorno.ToString();
        }


        public void RedefinirSenha(string email, string codigo, string novaSenha)
        {
            // Verifica se existe um usuário com email
            Usuario usuario = ctx.Usuario.Where(u => u.Email == email).FirstOrDefault();

            if (usuario == null)
                throw new ConstraintException("Usuário não cadastrado");

            if (usuario.Senha != codigo)
                throw new ConstraintException("Código inválido");

            
            //salva nova senha do usuário
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(novaSenha); ;

            ctx.Entry(usuario).Property(s => s.Senha).IsModified = true;

            ctx.SaveChanges();
        }

    }
}
