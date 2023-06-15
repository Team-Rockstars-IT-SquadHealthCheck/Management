using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;

namespace RockstarsManagementSquad.Services.Interfaces
{
    public interface IAnswerViewModelService
    {
        Task <List<AnswerViewModel>> UserAnswers(int id);
        Task <List<AnswerViewModel>> SquadSurveyAnswers(int squadId,int surveyId);
        Task<IEnumerable<AnswerViewModel>> GetSquadAnswers(int surveyId);
        Task<IEnumerable<AnswerViewModel>> GetAllAnswers();
        Task<IEnumerable<AnswerViewModel>> GetSquadFinnishedEnquetes(int squadId);
    }
}