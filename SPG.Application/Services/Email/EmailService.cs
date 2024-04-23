using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using SPG.Domain.Interfaces;

namespace SPG.Application.Services
{
  public class EmailService(IConfiguration configuration) : IEmailService
  {
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// Envia email
    /// </summary>
    public async Task SendEmailAsync(string email, string subject, string message)
    {
      var smtpClient = new SmtpClient(_configuration.GetSection("SmtpCredentials:ServerAddress").Value)
      {
        Port = int.Parse(_configuration.GetSection("SmtpCredentials:Port").Value ??  ""),
        Credentials = new NetworkCredential(_configuration.GetSection("SmtpCredentials:Username").Value, _configuration.GetSection("SmtpCredentials:Password").Value),
        EnableSsl = true,
      };

      var mailMessage = new MailMessage
      {
        From = new MailAddress(_configuration.GetSection("SmtpCredentials:FromEmail").Value ?? ""),
        Subject = subject,
        Body = message,
        IsBodyHtml = true,
      };

      mailMessage.To.Add(email);

      await smtpClient.SendMailAsync(mailMessage);
    }
  }
}
