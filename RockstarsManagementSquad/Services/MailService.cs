using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RockstarsManagementSquad.Configuration;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace RockstarsManagementSquad.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        
        public MailService(IOptions<MailSettings> mailSettingsOptions)
        {
            _mailSettings = mailSettingsOptions.Value;
        }

        public async Task<bool> SendMail(MailData mailData)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", mailData.EmailToAddress));
                message.Subject = mailData.EmailSubject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = mailData.EmailBody;
                bodyBuilder.HtmlBody = $"<html><body>{mailData.EmailBody}</body></html>";

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}