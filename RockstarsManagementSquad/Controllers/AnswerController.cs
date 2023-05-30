using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Controllers;

public class AnswerController : Controller
{
    private readonly Services.Interfaces.IAnswerViewModelService _service;
    private readonly Services.Interfaces.IRockstarViewModelService _rockstarViewModelService;
    private readonly ISquadViewModelService _squadViewModelService;

    public AnswerController(IAnswerViewModelService service, IRockstarViewModelService rockstarViewModelService, ISquadViewModelService squadViewModelService)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _rockstarViewModelService = rockstarViewModelService ?? throw new ArgumentNullException(nameof(service));
        _squadViewModelService = squadViewModelService ?? throw new ArgumentNullException((nameof(service)));
    }
    
    public async Task<IActionResult> User(int id)
    {
        var userAnswersTask = _service.UserAnswers(id);
        var rockstarTask = _rockstarViewModelService.FindById(id);

        // Await the completion of the two tasks
        var userAnswers = await userAnswersTask;
        var rockstar = await rockstarTask;
        //(bedankt ChatGPT)

        AnswerUserViewModel answerUserViewModel = new AnswerUserViewModel();
        answerUserViewModel.answers = userAnswers;
        answerUserViewModel.rockstar = rockstar;
        return View(answerUserViewModel);
    }

    public async Task<IActionResult> Squad(int id)
    {

		AnswerSquadViewModel squadAnswers = await GetAllSquadAnswersAsync(id);
		

		return View(squadAnswers);
    }

	public async Task<IActionResult> SquadGraph(int id)
	{
        AnswerSquadViewModel squadAnswers = await GetAllSquadAnswersAsync(id);


        return View(squadAnswers);
    }

    private async Task<AnswerSquadViewModel> GetAllSquadAnswersAsync(int id)
    {
		Task<List<AnswerViewModel>> squadAnswersTask = _service.SquadAnswers(id);
		var squadTask = _squadViewModelService.FindById(id);

        List<AnswerViewModel> SquadAnswers = await squadAnswersTask;
		var squad = await squadTask;


		Dictionary<int, AnswerUserViewModel> userAnswers = new Dictionary<int, AnswerUserViewModel>();


		foreach (AnswerViewModel answer in SquadAnswers)
		{
			if (!userAnswers.ContainsKey(answer.userid))
			{

				userAnswers[answer.userid] = new AnswerUserViewModel
				{
					userid = answer.userid,
					answers = new List<AnswerViewModel>()
				};
			}


			userAnswers[answer.userid].answers.Add(answer);
		}


		List<AnswerUserViewModel> userAnswersList = userAnswers.Values.ToList();


		var squadAnswers = new AnswerSquadViewModel
		{
			userAnswer = userAnswersList,
			squad = squad
		};

		foreach (var item in squadAnswers.userAnswer)
		{

			var rockstarTask = _rockstarViewModelService.FindById(item.userid);
			var rockstar = await rockstarTask;
			item.rockstar = rockstar;
		}

		return squadAnswers;
    }
    public async Task<IActionResult> CompanyGraph(int id)
    {
        //AnswerCompanyViewModel CompanyAnswers = await GetAllCompanyHappinessAsync(Companyid);
        GetAllCompanyHappinessAsync(id);

        return View();
    }
    //Task<AnswerCompanyViewModel>
    private async void GetAllCompanyHappinessAsync(int id)
    {
        Task<List<AnswerViewModel>> squadAnswersTask = _service.CompanyAnswers(id);
        var squadTask = _squadViewModelService.FindById(id);

        List<AnswerViewModel> SquadAnswers = await squadAnswersTask;
        var squad = await squadTask;


        Dictionary<int, AnswerUserViewModel> userAnswers = new Dictionary<int, AnswerUserViewModel>();


        foreach (AnswerViewModel answer in SquadAnswers)
        {
            if (!userAnswers.ContainsKey(answer.userid))
            {

                userAnswers[answer.userid] = new AnswerUserViewModel
                {
                    userid = answer.userid,
                    answers = new List<AnswerViewModel>()
                };
            }


            userAnswers[answer.userid].answers.Add(answer);
        }


        List<AnswerUserViewModel> userAnswersList = userAnswers.Values.ToList();


        var squadAnswers = new AnswerSquadViewModel
        {
            userAnswer = userAnswersList,
            squad = squad
        };

        foreach (var item in squadAnswers.userAnswer)
        {

            var rockstarTask = _rockstarViewModelService.FindById(item.userid);
            var rockstar = await rockstarTask;
            item.rockstar = rockstar;
        }

        //return squadAnswers;


    }

}