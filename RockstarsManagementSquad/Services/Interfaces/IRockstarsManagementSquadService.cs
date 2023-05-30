using RockstarsManagementSquad.Models;
namespace RockstarsManagementSquad.Services.Interfaces;

public interface IRockstarsManagementSquadService
{
    Task<IEnumerable<CompanyViewModel>> Find();
}