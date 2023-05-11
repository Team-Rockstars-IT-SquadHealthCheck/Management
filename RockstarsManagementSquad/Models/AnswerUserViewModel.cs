using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Models;

public class AnswerUserViewModel
{
	public int userid { get; set; }
	public RockstarViewModel rockstar { get; set; }
    public List<AnswerViewModel> answers { get; set; }
}