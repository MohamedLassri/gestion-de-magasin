using Microsoft.AspNetCore.Identity.UI.Services;

namespace gestion_de_magasin.Data
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // On ne fait rien, juste pour que ça ne plante plus
            return Task.CompletedTask;
        }
    }
}