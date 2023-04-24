using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;

namespace RockstarsManagementSquad.Services.Interfaces;

public interface IRockstarViewModelService
{
    Task<IEnumerable<RockstarViewModel>> Find();
    Task<RockstarViewModel> Create(UserDTO user);
}