using RockstarsManagementSquad.Models;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Helpers
{
    public static class Extensions
    {
        public static User ConvertRockstarViewModelToUser(this RockstarViewModel rvm)
        {
            return new User(rvm.id, rvm.username, rvm.email, rvm.roleid, rvm.squadid, rvm.url);
        }
    }
}
