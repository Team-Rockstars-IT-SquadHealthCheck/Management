using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Models;

namespace RockstarsManagementSquad.Services.Interfaces
{
    public interface IUserViewModelService
    {
        Task<string> AddSurveyLinkToUser(string SurveyLink, int userId);
    }
}
