using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Models
{
    public class SquadViewModel
    {
        public int id { get; set; }
        public int surveyId { get; set; }
        public int? companyId { get; set; }
        public string? companyName { get; set; }
        public string name { get; set; }
    }
}
