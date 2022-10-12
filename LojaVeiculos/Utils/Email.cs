using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace LojaVeiculos.Utils
{
    public static class Email
    {

            public static async Task Execute()
            {
                var apiKey = "APIKeyLojaVeiculos";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("vanbarone.milani@gmail.com", "Vanessa");
                var subject = "Teste Envio de email com SendGrid";
                var to = new EmailAddress("saraalcaras13@gmail.com", "Germana");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
        
    }

}
