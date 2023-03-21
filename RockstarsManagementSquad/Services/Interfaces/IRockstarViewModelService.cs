using RockstarsManagementSquad.Models;
namespace RockstarsManagementSquad.Services.Interfaces;

public interface IRockstarViewModelService
{
    Task<IEnumerable<RockstarViewModel>> Find();   
}