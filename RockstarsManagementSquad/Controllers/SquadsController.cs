using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;

namespace RockstarsManagementSquad.Controllers;

public class SquadsController : Controller   
{
    private readonly Services.Interfaces.ISquadViewModelService _service;

    public SquadsController(RockstarsManagementSquad.Services.Interfaces.ISquadViewModelService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public async Task<IActionResult> Index()
    {
        //var squads = await _service.Find();
        var products = await _service.FindAll();
        
        
        return View(products);
    }

    public async Task<IActionResult> Info(int? id)
    {
        var squad = await _service.FindById(id);
        var users = await _service.UsersInSquad(id);
        SquadInfoViewModel infoViewModel = new SquadInfoViewModel();
        infoViewModel.squad = squad;
        infoViewModel.users = users;

        return View(infoViewModel);
    }

    public async Task<IActionResult> Results()
    {
        return View();
    }

    public async Task<IActionResult> Delete()
    {
        return View();
    }
}