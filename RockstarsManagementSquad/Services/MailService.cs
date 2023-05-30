using Microsoft.Extensions.Options;
using PostmarkDotNet;
using PostmarkDotNet.Model;
using RockstarsManagementSquad.Configuration;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;

namespace RockstarsManagementSquad.Services;

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
            var message = new PostmarkMessage()
            {
                To = mailData.EmailToAddress,
                From = _mailSettings.SenderEmail,
                TrackOpens = true,
                Subject = mailData.EmailSubject,
                TextBody = $"{mailData.EmailBody}",
                HtmlBody = $"<html><body>{mailData.EmailBody}</body></html>",
                MessageStream = "broadcast"
            };

            var client = new PostmarkClient(_mailSettings.ServerToken);
            var sendResult = await client.SendMessageAsync(message);

            return true;
        }
        catch (Exception ex)
        {
            // Exception Details
            return false;
        }
    }
}
