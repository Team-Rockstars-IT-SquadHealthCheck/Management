using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;

namespace RockstarsManagementSquad.Services;

public class AnswerViewModelService : IAnswerViewModelService
{
    private readonly HttpClient _client;
    public const string BasePath = "/";
    
    public AnswerViewModelService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
    
    // public async Task<IEnumerable<AnswerViewModel>> Find()
    // {
    //     string path = "https://localhost:7259/";
    //     var response = await _client.GetAsync(path); // path was BasePath
    //
    //     return await response.ReadContentAsync<List<AnswerViewModel>>();
    // }
}