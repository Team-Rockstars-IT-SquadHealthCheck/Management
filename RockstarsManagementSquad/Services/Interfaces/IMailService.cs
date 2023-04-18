using RockstarsManagementSquad.Models;

namespace RockstarsManagementSquad.Services.Interfaces;

public interface IMailService
{
    bool SendMail(MailData mailData);
}