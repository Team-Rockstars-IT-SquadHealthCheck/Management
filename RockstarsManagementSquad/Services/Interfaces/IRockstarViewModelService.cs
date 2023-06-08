using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;

namespace RockstarsManagementSquad.Services.Interfaces;

public interface IRockstarViewModelService
{
    Task<IEnumerable<RockstarViewModel>> Find();
    Task<RockstarViewModel> Create(UserDTO user);
    Task<RockstarViewModel> FindById(int userId);
    Task<RockstarViewModel> Delete(int userId);
    Task<bool> UpdateUserSquadID(int squadId, int userId);
    Task<bool> RemoveUserFromSquad(int userId);
}