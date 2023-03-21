using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;
using System.Text.Json.Nodes;

namespace RockstarsManagementSquad.Services;

/// <summary>
/// Now we will create the class that will implement the previous Interface.
/// It will also consume the API endpoint through the route represented by the “BasePath” variable.
/// </summary>
public class RockstarViewModelService : IRockstarViewModelService
{
    private readonly HttpClient _client;
    public const string BasePath = "/";

    public RockstarViewModelService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<RockstarViewModel>> Find()
    {
        string path = "https://localhost:7259/users";
        var response = await _client.GetAsync(path); // path was BasePath

        return await response.ReadContentAsync<List<RockstarViewModel>>();
    }
}