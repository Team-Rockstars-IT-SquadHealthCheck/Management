using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Models;

public class AnswerUserViewModel
{
    public RockstarViewModel rockstar { get; set; }
    public List<AnswerViewModel> answers { get; set; }
}