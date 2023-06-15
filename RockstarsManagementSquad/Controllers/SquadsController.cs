using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Controllers;

public class SquadsController : Controller   
{
    private readonly Services.Interfaces.ISquadViewModelService _squad;
    private readonly Services.Interfaces.IAnswerViewModelService _answer;
    private readonly Services.Interfaces.ICompanyViewModelService _company;
    private readonly Services.Interfaces.IRockstarViewModelService _rockstar;


	public SquadsController(RockstarsManagementSquad.Services.Interfaces.ISquadViewModelService squad,
        RockstarsManagementSquad.Services.Interfaces.IAnswerViewModelService answer,
		RockstarsManagementSquad.Services.Interfaces.ICompanyViewModelService company,
		RockstarsManagementSquad.Services.Interfaces.IRockstarViewModelService rockstar)
    {

		_squad = squad ?? throw new ArgumentNullException(nameof(squad));
        _answer = answer ?? throw new ArgumentNullException(nameof(answer));
        _company = company ?? throw new ArgumentNullException(nameof(company));
		_rockstar = rockstar ?? throw new ArgumentNullException(nameof(rockstar));
	}

    public async Task<IActionResult> Index()
    {
        //var squads = await _service.Find();
        var squads = await _squad.FindAll();
        var companies = await _company.Find();
        var users = await _rockstar.Find();

        List<SelectListItem> companyItems = companies.Select(c => new SelectListItem
        {
            Text = c.name.ToString(),
            Value = c.id.ToString()
        }).ToList();

		List<SelectListItem> SquadItems = squads.Select(c => new SelectListItem
		{
			Text = c.name.ToString(),
			Value = c.id.ToString()
		}).ToList(); 

        List<SelectListItem> UserItems = users.Select(c => new SelectListItem
		{
			Text = c.username.ToString(),
			Value = c.id.ToString()
		}).ToList();

		ViewBag.Company = companyItems;
        ViewBag.Squad = SquadItems;
        ViewBag.User = UserItems;


        return View(squads);
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

    [HttpPost]
    public async Task<IActionResult> Create(string squadName, int companyid)
    {
        CreateSquadDTO squadDTO = new CreateSquadDTO {
            CompanyId = companyid,
            name = squadName,
            CompanyName = "",
            Id = 0
        };

        _squad.Create(squadDTO);


        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Add(int squadId, int userId)
    {
        _rockstar.UpdateUserSquadID(squadId, userId);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RemoveUserFromSquad(int userId, int squadId)
    {
        _rockstar.RemoveUserFromSquad(userId);
        return RedirectToAction("Info", "Squads", new { id = squadId});
    }

    public async Task<IActionResult> Delete(int squadId)
    {
        _squad.DeleteSquad(squadId);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Graph(int squadid)
    {
		var results = await _answer.SquadAnswers(squadid);
		return View(results);
    }

    public async Task<IActionResult> RemoveSquadFromCompany(int squadId, int companyId)
    {
        _squad.RemoveSquadFromCompany(squadId);
        return RedirectToAction("Info", "Companies", new { id = companyId });
    }
}