using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;
using System.Text.Json.Nodes;

namespace RockstarsManagementSquad.Services;

/// <summary>
/// Now we will create the class that will implement the previous Interface.
/// It will also consume the API endpoint through the route represented by the “BasePath” variable.
/// </summary>
public class CompanyViewModelService : ICompanyViewModelService
{
    private readonly HttpClient _client;
    public const string BasePath = "/";

    public CompanyViewModelService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<CompanyViewModel>> Find()
    {
        string path = "https://localhost:7259/companies";
        var response = await _client.GetAsync(path); // path was BasePath

        return await response.ReadContentAsync<List<CompanyViewModel>>();
    }

    public async Task<CompanyViewModel> FindById(int? companyId)
    {
        string path = $"https://localhost:7259/CompanyDetails/{companyId}";
        var response = await _client.GetAsync(path); // path was BasePath

        return await response.ReadContentAsync<CompanyViewModel>();
    }

    public async Task<List<SquadViewModel>> SquadsInCompany(int? companyId)
    {
        string path = $"https://localhost:7259/SquadsInCompany/{companyId}";
        var response = await _client.GetAsync(path); // path was BasePath

        return await response.ReadContentAsync<List<SquadViewModel>>();
    }

    public async Task<IEnumerable<SquadViewModel>> SquadsInCompany(int companyId)
    {
        string path = $"https://localhost:7259/SquadsInCompany/{companyId}";
        var response = await _client.GetAsync(path); // path was BasePath

        return await response.ReadContentAsync<List<SquadViewModel>>();
    }

    public async Task<CompanyViewModel> Create(CompanyDTO company)
    {
        string path = "https://localhost:7259/Company";
        var response = await _client.PostAsJsonAsync<CompanyDTO>(path, company); // path was BasePath

        return await response.ReadContentAsync<CompanyViewModel>();
    }

    public async Task<bool> Delete(int id)
    {
        string path = $"https://localhost:7259/api/Company/{id}/Delete";
        var response = await _client.DeleteAsync(path); // path was BasePath

        return response.IsSuccessStatusCode;
    }
}
