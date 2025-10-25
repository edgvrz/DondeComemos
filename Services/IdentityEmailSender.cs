using Microsoft.AspNetCore.Identity.UI.Services;

namespace DondeComemos.Services
{
    public class IdentityEmailSender : IEmailSender
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<IdentityEmailSender> _logger;

        public IdentityEmailSender(IEmailService emailService, ILogger<IdentityEmailSender> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                await _emailService.SendEmailAsync(email, subject, htmlMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar email a {email}: {ex.Message}");
                // No lanzar excepción para no bloquear el registro
            }
        }
    }
}