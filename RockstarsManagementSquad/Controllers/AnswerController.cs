using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;

namespace RockstarsManagementSquad.Controllers;

public class AnswerController : Controller
{
    private readonly Services.Interfaces.IAnswerViewModelService _service;
    private readonly Services.Interfaces.IRockstarViewModelService _rockstarViewModelService;

    public AnswerController(IAnswerViewModelService service, IRockstarViewModelService rockstarViewModelService)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _rockstarViewModelService = rockstarViewModelService ?? throw new ArgumentNullException(nameof(service));
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

}