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
    public class LoginRepositorie : ILoginRepositorie
    {
        LojaVeiculosContext ctx;

        public LoginRepositorie(LojaVeiculosContext _ctx)
        {
            ctx = _ctx;
        }

        public string Logar(string email, string senha)
        {
            var usuario = ctx.Usuario.FirstOrDefault(u => u.Email == email && u.Senha == senha);

            if (usuario != null)
            {
                bool confere = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);
                if (confere)
                {
                    var minhasClaims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Adm"),

                    new Claim("Cargo", "Adm")
                    };

                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("lojaVeiculos-chave-autenticacao"));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var meuToken = new JwtSecurityToken(
                        issuer: "lojaVeiculos.webAPI",
                        audience: "lojaVeiculos.webAPI",
                        claims: minhasClaims,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: creds
                        );
                    
                    return new JwtSecurityTokenHandler().WriteToken(meuToken);
                }
            }
            return null;
        }
    }
}
