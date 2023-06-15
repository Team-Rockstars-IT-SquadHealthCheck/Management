using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Services.Interfaces;

/// <summary>
/// The service class will be responsible for implementing the methods available in the client we just created—in this case,
/// fetching the data through the API.
/// </summary>
public interface ISquadViewModelService
{
    Task<IEnumerable<SquadViewModel>> FindAll();
    Task<SquadViewModel> FindById(int? id);
    Task<List<RockstarViewModel>> UsersInSquad(int? id);
    Task<IEnumerable<RockstarViewModel>> UsersInSquad(int squadId);
	Task<CreateSquadDTO> Create(CreateSquadDTO squadDTO);
    Task<bool> DeleteSquad(int squadId);
    Task<bool> RemoveSquadFromCompany(int squadId);
    Task<bool> AddSquadToCompany(int squadId, int companyId);
    Task<List<SurveyViewModel>> GetAllSquadSurveys(int squadId);
}