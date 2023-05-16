using Newtonsoft.Json.Linq;

namespace RockstarsManagementSquad.Models
{
    public class UserAnswerViewModel
    {
        public int userid { get; set; }
        public List<AnswerViewModel> answers { get; set; }

    }
}
