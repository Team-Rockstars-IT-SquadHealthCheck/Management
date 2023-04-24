using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;
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

    public async Task<IEnumerable<SquadViewModel>> FindAll()
    {
        string path = "https://localhost:7259/squads";
        var response = await _client.GetAsync(path); // path was BasePath

        return await response.ReadContentAsync<List<SquadViewModel>>();
    }


  public async Task<SquadViewModel> FindById(int? squadId)
  {
		string path = $"https://localhost:7259/SquadDetails/{squadId}";
		var response = await _client.GetAsync(path); // path was BasePath

		return await response.ReadContentAsync<SquadViewModel>();
	}

	public async Task<List<UserViewModel>> UsersInSquad(int? squadId)
	{
		string path = $"https://localhost:7259/UsersInSquad/{squadId}";
		var response = await _client.GetAsync(path); // path was BasePath

		return await response.ReadContentAsync<List<UserViewModel>>();
	}


	public async Task<IEnumerable<UserViewModel>> UsersInSquad(int squadId)
    {
        string path = $"https://localhost:7259/UsersInSquad/{squadId}";
        var response = await _client.GetAsync(path); // path was BasePath

        return await response.ReadContentAsync<List<RockstarViewModel>>();
    }
}