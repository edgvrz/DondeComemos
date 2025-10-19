using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;


namespace DondeComemos.Services
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"[FAKE EMAIL] A: {email} | Asunto: {subject} | Contenido: {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}