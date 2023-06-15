using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Services.Interfaces;

/// <summary>
/// The service class will be responsible for implementing the methods available in the client we just created—in this case,
/// fetching the data through the API.
/// </summary>
public interface ICompanyViewModelService
{
    Task<IEnumerable<CompanyViewModel>> Find();
    Task<CompanyViewModel> Create(CompanyDTO company);
    Task<CompanyViewModel> FindById(int? id);
    Task<List<SquadViewModel>> SquadsInCompany(int? companyId);
    Task<IEnumerable<SquadViewModel>> SquadsInCompany(int companyId);
    Task<bool> Delete(int id);
}

