using RockstarsManagementSquad.Models;
namespace RockstarsManagementSquad.Services.Interfaces;

/// <summary>
/// The service class will be responsible for implementing the methods available in the client we just created—in this case,
/// fetching the data through the API.
/// </summary>
public interface ISquadViewModelService
{
    Task<IEnumerable<SquadViewModel>> Find();
}