using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Threading.Tasks;

namespace DondeComemos.Services
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"[FAKE EMAIL] Para: {email}");
            Console.WriteLine($"Asunto: {subject}");
            Console.WriteLine($"Mensaje: {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}
