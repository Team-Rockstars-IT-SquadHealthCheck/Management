using System.Diagnostics.Contracts;

namespace RockstarsManagementSquad.Models
{
    public class SquadAnswerViewModel
    {
        public int squadid { get; set; }
        public List<UserAnswerViewModel> answers { get; set; }
    }
}
