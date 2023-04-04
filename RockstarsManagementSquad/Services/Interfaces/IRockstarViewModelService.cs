using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;

namespace RockstarsManagementSquad.Services.Interfaces;

public interface IRockstarViewModelService
{
    Task<IEnumerable<RockstarViewModel>> Find();
    Task<UserViewModel> Create(UserDTO user);
}