using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace LojaVeiculos.Utils
{
    public class Email
    {

            public static async Task Execute()
            {
            //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
            
                var apiKey = "LojaVeiculos";
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
