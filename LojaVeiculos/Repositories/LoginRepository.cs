using LojaVeiculos.Context;
using LojaVeiculos.Interfaces;
using LojaVeiculos.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
            var usuario = ctx.Usuario.FirstOrDefault(u => u.Email == email);

            if (usuario != null && senha != null && usuario.Senha.Contains("$2b$"))
            {
                if (BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
                {
                    IRepository<Usuario> repoUsuario = new UsuarioRepository(ctx);
                    
                    usuario = repoUsuario.FindById(usuario.Id);
                    

                    //*** Credenciais do JWT para geração do token ***

                    //definição das claims
                    var minhasClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,  usuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, usuario.TipoUsuario.Tipo.ToUpper()),
                        new Claim("Tipo", usuario.TipoUsuario.Tipo.ToUpper())
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
    }
}
