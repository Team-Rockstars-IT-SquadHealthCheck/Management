namespace RockstarsManagementSquad.Services.Interfaces
{
    public interface IUserViewModelService
    {
        Task<string> AddSurveyLinkToUser(string SurveyLink, int userId);
    }
}
