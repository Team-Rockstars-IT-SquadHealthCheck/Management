namespace RockstarsManagementSquad.Models
{
    public class DashboardViewModel
    {
        public List<SquadViewModel> SquadFinnishedEnquetes { get; set; }
        public List<SquadViewModel> SquadNotFinnishedEnquetes { get; set; }

        public List<int> UsersInSquads { get; set; }
        public List<int> AnswersInSquads { get; set; }
    }
}
