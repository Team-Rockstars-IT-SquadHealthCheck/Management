using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;

namespace RockstarsManagementSquad.Services.Interfaces
{
    public interface IAnswerViewModelService
    {
        Task <IEnumerable<AnswerViewModel>> GetUserAswers(int userId);
        Task<IEnumerable<AnswerViewModel>> GetSquadAnswers(int squadId);
        Task<IEnumerable<AnswerViewModel>> GetAllAnswers();
    }
}