using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;

namespace RockstarsManagementSquad.Services.Interfaces
{
    public interface IAnswerViewModelService
    {
        Task <List<AnswerViewModel>> UserAnswers(int id);
        Task <List<AnswerViewModel>> SquadAnswers(int id);
        Task<IEnumerable<AnswerViewModel>> GetAllAnswers();
    }
}