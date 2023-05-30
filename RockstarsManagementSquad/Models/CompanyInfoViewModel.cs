using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Models
{
    public class CompanyInfoViewModel
    {
        public CompanyViewModel company { get; set; }
        public List<SquadViewModel> squads { get; set; }
    }
}
