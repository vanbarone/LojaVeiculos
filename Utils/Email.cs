using LojaVeiculos.Models;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;

namespace LojaVeiculos.Utils
{
    public static class Email
    {

        public static async Task<string> Execute(string email, string nome, string codigo)
        {
            var apiKey = Environment.GetEnvironmentVariable("LojaVeiculosAPIKey");

            if (apiKey == null)
                return "APIKey não encontrada";
            else
            {
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("fernandotm01@gmail.com", "LojaVeiculos");
                var subject = "Recuperação de Senha";
                var to = new EmailAddress(email);
                var plainTextContent = "Teste";
                
                var htmlContent = $"<h4><font color=\"blue\">{nome}</font>, você iniciou o processo para redefinição de sua senha </h4> " +
                                  $"<h4>Utilize o código <font color=\"blue\">{codigo}</font> para cocluir esse processo</h4>";


                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent , htmlContent);

                var response = await client.SendEmailAsync(msg);

                if (response.IsSuccessStatusCode)
                    return "";
                else
                    return response.StatusCode.ToString();
            }
        }
        
    }

}
