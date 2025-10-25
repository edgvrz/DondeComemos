using System.Net;
using System.Net.Mail;

namespace DondeComemos.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendWelcomeEmailAsync(string to, string userName);
        Task SendReviewNotificationAsync(string to, string userName, string restaurantName);
        Task SendPasswordResetEmailAsync(string to, string resetLink);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var smtpServer = _configuration["Email:SmtpServer"] ?? "smtp.gmail.com";
                var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
                var smtpUsername = _configuration["Email:SmtpUsername"];
                var smtpPassword = _configuration["Email:SmtpPassword"];
                var fromEmail = _configuration["Email:FromEmail"] ?? smtpUsername;
                var fromName = _configuration["Email:FromName"] ?? "DondeComemos";

                if (string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword))
                {
                    _logger.LogWarning("Email credentials not configured. Email not sent.");
                    return;
                }

                using var client = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail!, fromName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage);
                _logger.LogInformation($"Email sent successfully to {to}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email to {to}: {ex.Message}");
                throw;
            }
        }

        public async Task SendWelcomeEmailAsync(string to, string userName)
        {
            var subject = "¡Bienvenido a DondeComemos!";
            var body = $@"
                <html>
                <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                        <h1 style='color: #0d6efd; text-align: center;'>¡Bienvenido a DondeComemos!</h1>
                        <p>Hola <strong>{userName}</strong>,</p>
                        <p>Gracias por registrarte en DondeComemos, tu guía confiable para descubrir los mejores restaurantes de Arequipa.</p>
                        <h3>¿Qué puedes hacer ahora?</h3>
                        <ul>
                            <li>🔍 Explora restaurantes cerca de ti</li>
                            <li>⭐ Deja reseñas y calificaciones</li>
                            <li>❤️ Guarda tus restaurantes favoritos</li>
                            <li>📍 Encuentra ubicaciones con mapas interactivos</li>
                        </ul>
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='https://localhost:7230' style='background-color: #0d6efd; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                                Comenzar a Explorar
                            </a>
                        </div>
                        <p style='color: #666; font-size: 12px; text-align: center; margin-top: 30px;'>
                            © {DateTime.Now.Year} DondeComemos. Todos los derechos reservados.
                        </p>
                    </div>
                </body>
                </html>";

            await SendEmailAsync(to, subject, body);
        }

        public async Task SendReviewNotificationAsync(string to, string userName, string restaurantName)
        {
            var subject = $"Nueva reseña en {restaurantName}";
            var body = $@"
                <html>
                <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                        <h2 style='color: #0d6efd;'>Nueva Reseña Publicada</h2>
                        <p>Hola <strong>{userName}</strong>,</p>
                        <p>Se ha publicado una nueva reseña en <strong>{restaurantName}</strong>.</p>
                        <p>¡No olvides revisar las opiniones de otros comensales!</p>
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='https://localhost:7230/Restaurantes/Search' style='background-color: #28a745; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                                Ver Restaurantes
                            </a>
                        </div>
                        <p style='color: #666; font-size: 12px; text-align: center; margin-top: 30px;'>
                            © {DateTime.Now.Year} DondeComemos. Todos los derechos reservados.
                        </p>
                    </div>
                </body>
                </html>";

            await SendEmailAsync(to, subject, body);
        }

        public async Task SendPasswordResetEmailAsync(string to, string resetLink)
        {
            var subject = "Restablecer tu contraseña - DondeComemos";
            var body = $@"
                <html>
                <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
                    <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                        <h2 style='color: #dc3545;'>Restablecer Contraseña</h2>
                        <p>Has solicitado restablecer tu contraseña en DondeComemos.</p>
                        <p>Haz clic en el siguiente botón para continuar:</p>
                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='{resetLink}' style='background-color: #dc3545; color: white; padding: 12px 30px; text-decoration: none; border-radius: 5px; display: inline-block;'>
                                Restablecer Contraseña
                            </a>
                        </div>
                        <p style='color: #dc3545; font-size: 14px;'><strong>Nota:</strong> Este enlace expirará en 24 horas.</p>
                        <p style='color: #666; font-size: 12px;'>Si no solicitaste este cambio, ignora este correo.</p>
                        <p style='color: #666; font-size: 12px; text-align: center; margin-top: 30px;'>
                            © {DateTime.Now.Year} DondeComemos. Todos los derechos reservados.
                        </p>
                    </div>
                </body>
                </html>";

            await SendEmailAsync(to, subject, body);
        }
    }
}