using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;

namespace RockstarsManagementSquad.Controllers;

public class SquadsController : Controller   
{
    private readonly Services.Interfaces.ISquadViewModelService _squad;
    private readonly Services.Interfaces.IAnswerViewModelService _answer;

    public SquadsController(RockstarsManagementSquad.Services.Interfaces.ISquadViewModelService squad,
        RockstarsManagementSquad.Services.Interfaces.IAnswerViewModelService answer)
    {
        _squad = squad ?? throw new ArgumentNullException(nameof(squad));
        _answer = answer ?? throw new ArgumentNullException(nameof(answer));
    }

    public async Task<IActionResult> Index()
    {
        //var squads = await _service.Find();
        var products = await _squad.FindAll();
        
        
        return View(products);
    }

    public async Task<IActionResult> Info(int? id)
    {
        var squad = await _squad.FindById(id);
        var rockstars = await _squad.UsersInSquad(id);
        SquadInfoViewModel infoViewModel = new SquadInfoViewModel();
        infoViewModel.squad = squad;
        infoViewModel.rockstars = rockstars;

        return View(infoViewModel);
    }

    public async Task<IActionResult> Results(int id)
    {
        var results = await _answer.SquadAnswers(id);

        return View();
    }

    public SquadAnswerViewModel SortAnswersToUser()
    {
        return null;
    }

    public async Task<IActionResult> Delete()
    {
        return View();
    }
    public async Task<IActionResult> Graph(int squadid)
    {
		var results = await _answer.SquadAnswers(squadid);
		return View(results);
    }
}