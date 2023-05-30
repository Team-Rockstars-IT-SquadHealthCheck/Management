using RockstarsManagementSquad.Models;

namespace RockstarsManagementSquad.Services.Interfaces;

public interface IMailService
{
    Task<bool> SendMail(MailData mailData);
}