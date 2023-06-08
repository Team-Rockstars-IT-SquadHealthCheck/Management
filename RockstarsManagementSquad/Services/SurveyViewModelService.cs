using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace RockstarsManagementSquad.Services;

/// <summary>
/// Now we will create the class that will implement the previous Interface.
/// It will also consume the API endpoint through the route represented by the “BasePath” variable.
/// </summary>
public class SurveyViewModelService : ISurveyViewModelService
{
    private readonly HttpClient _client;
    public const string BasePath = "/";

    public SurveyViewModelService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<SurveyDTO>> Find()
    {
        string path = "https://localhost:7259/surveys";
        var response = await _client.GetAsync(path); // path was BasePath

        return await response.ReadContentAsync<List<SurveyDTO>>();
    }

    public async Task<SurveyDTO> FindById(int surveyId)
    {
        string path = "https://localhost:7259/SurveyDetails/"+surveyId;
        var response = await _client.GetAsync(path); // path was BasePath

        return await response.ReadContentAsync<SurveyDTO>();
    }

    public async Task<SurveyDTO> Create(SurveyDTO surveyDTO)
    {
        string path = "https://localhost:7259/api/survey";
        var response = await _client.PostAsJsonAsync<SurveyDTO>(path, surveyDTO); // path was BasePath

        return await response.ReadContentAsync<SurveyDTO>();
    }

    public async Task<bool> CreateLinkSurveySquad(LinkSurveySquadDTO linkSurveySquadDTO)
    {
        string path = "https://localhost:7259/api/LinkSurveySquad";
        var response = await _client.PostAsJsonAsync<LinkSurveySquadDTO>(path, linkSurveySquadDTO); // path was BasePath

        return await response.ReadContentAsync<SurveyDTO>() != null;
    }
    public async void CreateQuestion(QuestionDTO questionDTO)
    {
        string path = "https://localhost:7259/Question";
        await _client.PostAsJsonAsync(path, questionDTO); // path was BasePath
    }
}
