using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;
using System.Runtime.CompilerServices;
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


	public async Task<CreateSquadDTO> Create(CreateSquadDTO squadDTO)
	{
		string path = "https://localhost:7259/api/Squad";
		var response = await _client.PostAsJsonAsync<CreateSquadDTO>(path, squadDTO); // path was BasePath

		return await response.ReadContentAsync<CreateSquadDTO>();
	}
	public async Task<SquadViewModel> FindById(int? squadId)
	{
		string path = $"https://localhost:7259/SquadDetails/{squadId}";
		var response = await _client.GetAsync(path); // path was BasePath

		return await response.ReadContentAsync<SquadViewModel>();
	}

	public async Task<List<RockstarViewModel>> UsersInSquad(int? squadId)
	{
		string path = $"https://localhost:7259/UsersInSquad/{squadId}";
		var response = await _client.GetAsync(path); // path was BasePath

		return await response.ReadContentAsync<List<RockstarViewModel>>();
	}


	public async Task<IEnumerable<RockstarViewModel>> UsersInSquad(int squadId)
	{
		string path = $"https://localhost:7259/UsersInSquad/{squadId}";
		var response = await _client.GetAsync(path); // path was BasePath

		return await response.ReadContentAsync<List<RockstarViewModel>>();
	}

	public async Task<bool> DeleteSquad(int id)
	{	
        string path = $"https://localhost:7259/api/Squad/{id}/Delete";
        var response = await _client.DeleteAsync(path);

        return response.IsSuccessStatusCode; 
    }

	public async Task<bool> RemoveSquadFromCompany(int squadId)
	{
		string path = $"https://localhost:7259/api/Squad/{squadId}/CompanyRemove";
        var response = await _client.PostAsync(path, null);

		return response.IsSuccessStatusCode;
    }

	public async Task<bool> AddSquadToCompany(int squadId, int companyId)
	{
        string path = $"https://localhost:7259/api/Squad/{squadId}/Company/{companyId}";
        var response = await _client.PostAsync(path, null);

        return response.IsSuccessStatusCode;
    }

	public async Task<List<SurveyViewModel>> GetAllSquadSurveys(int squadId)
	{
		string path = $"https://localhost:7259/Squad/{squadId}/Surveys";
		var response = await _client.GetAsync(path); // path was BasePath

		return await response.ReadContentAsync<List<SurveyViewModel>>();
	}
}	