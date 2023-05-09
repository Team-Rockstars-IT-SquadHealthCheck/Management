using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;

namespace RockstarsManagementSquad.Services.Interfaces
{
    public interface IAnswerViewModelService
    {
        Task <List<AnswerViewModel>> UserAnswers(int id);
        Task <IEnumerable<AnswerViewModel>> SquadAnswers(int id);
    }
}