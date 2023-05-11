using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Models;

public class AnswerUserViewModel
{
	public int userid { get; set; }
	public RockstarViewModel rockstar { get; set; }
    public List<AnswerViewModel> answers { get; set; }
	public int happyAmount { get { return CalculateHappy(); } }
	public int neutralAmount { get { return CalculateNeutral(); } }
	public int sadAmount { get { return CalculateSad(); } }

	public int avarageHappiness { get { return CalculateHappiness(); } }

	private int CalculateHappy()
	{
		var happy = 0;
		foreach (var answer in answers)
		{
			if (answer.answer == 0)
			{
				happy += 1;
			}
		}
		return happy;
	}
	private int CalculateNeutral()
	{
		var neutral = 0;
		foreach (var answer in answers)
		{
			if (answer.answer == 1)
			{
				neutral += 1;
			}
		}
		return neutral;
	}
	private int CalculateSad()
	{
		var sad = 0;
		foreach (var answer in answers)
		{
			if (answer.answer == 2)
			{
				sad += 1;
			}
		}
		return sad;
	}
	private int CalculateHappiness()
	{
		float happy = CalculateHappy() * 0;
		float neutral = CalculateNeutral() * 1;
		float sad = CalculateSad() * 2;
		float total = happy + neutral + sad;
		float amountOfQuestion = answers.Count;
		float happiness = total / amountOfQuestion;

		if (happiness <= 0.633)
		{
			return 0;
		}
		else if (happiness > 0.633 && happiness <= 1.266)
		{
			return 1;
		}
		else
		{
			return 2;
		}
	}
}
