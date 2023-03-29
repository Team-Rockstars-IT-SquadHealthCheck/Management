using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Services.Interfaces;

/// <summary>
/// The service class will be responsible for implementing the methods available in the client we just created—in this case,
/// fetching the data through the API.
/// </summary>
public interface ISurveyViewModelService
{
    Task<IEnumerable<SurveyViewModel>> Find();
    //Task<string> CreateSurveyLink(string SurveyLink);
}

