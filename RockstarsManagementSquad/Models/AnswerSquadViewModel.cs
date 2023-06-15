namespace RockstarsManagementSquad.Models;

public class AnswerSquadViewModel
{
    public SquadViewModel squad { get; set; }
    public List<AnswerUserViewModel> userAnswer { get; set; }
    public int TotalSad { get { return userAnswer.Sum(x => x.sadAmount); } }
    public int TotalMeh { get { return userAnswer.Sum(x => x.neutralAmount); } }
    public int TotalGood { get { return userAnswer.Sum(x => x.happyAmount); } }
}