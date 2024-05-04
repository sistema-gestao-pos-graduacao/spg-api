using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using SPG.Domain.Interfaces;
using SPG.Domain.SystemParams;

namespace SPG.Application.Services
{
  public class EmailService(SystemParams systemParams) : IEmailService
  {
    private readonly SystemParams _systemParams = systemParams;

    /// <summary>
    /// Envia email
    /// </summary>
    public async Task SendEmailAsync(string email, string subject, string message)
    {
      var smtpClient = new SmtpClient(_systemParams.SmtpServerAddress)
      {
        Port = int.Parse(_systemParams.SmtpPort),
        Credentials = new NetworkCredential(_systemParams.SmtpUsername, _systemParams.SmtpPassword),
        EnableSsl = true,
      };

      var mailMessage = new MailMessage
      {
        From = new MailAddress(_systemParams.SmtpFromEmail),
        Subject = subject,
        Body = message,
        IsBodyHtml = true,
      };

      mailMessage.To.Add(email);

      await smtpClient.SendMailAsync(mailMessage);
    }
  }
}
