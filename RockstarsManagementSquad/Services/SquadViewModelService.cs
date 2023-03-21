using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;
using System.Text.Json.Nodes;

namespace RockstarsManagementSquad.Services;

/// <summary>
/// Now we will create the class that will implement the previous Interface.
/// It will also consume the API endpoint through the route represented by the “BasePath” variable.
/// </summary>
public class SquadViewModelService : ISquadViewModelService
{
    private readonly HttpClient _client;
    public const string BasePath = "/";

    public SquadViewModelService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<SquadViewModel>> Find()
    {
        string path = "https://localhost:7259/squads";
        var response = await _client.GetAsync(path); // path was BasePath

        return await response.ReadContentAsync<List<SquadViewModel>>();
    }
}