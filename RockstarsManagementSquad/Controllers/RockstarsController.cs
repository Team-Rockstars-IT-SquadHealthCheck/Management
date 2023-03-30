using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;

namespace RockstarsManagementSquad.Controllers;

public class RockstarsController : Controller   
{
    private readonly Services.Interfaces.IRockstarViewModelService _rockstarsService;

    private readonly Services.Interfaces.ISquadViewModelService _squadService;

    public RockstarsController(RockstarsManagementSquad.Services.Interfaces.IRockstarViewModelService service, RockstarsManagementSquad.Services.Interfaces.ISquadViewModelService _service)
    {
        _rockstarsService = service ?? throw new ArgumentNullException(nameof(service));
        _squadService = _service ?? throw new ArgumentNullException(nameof(_service));
    }

    //public RockstarsController(RockstarsManagementSquad.Services.Interfaces.ISquadViewModelService service)
    //{
    //    _squadService = service ?? throw new ArgumentNullException(nameof(service));
    //}

    public async Task<IActionResult> Index()
    {
        //var squads = await _service.Find();
        var rockstarProducts = await _rockstarsService.Find();

        return View(rockstarProducts);
    }

    

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        List<SquadViewModel> squads = new List<SquadViewModel>();

        var squadProducts = await _squadService.Find();

        foreach (var squadProduct in squadProducts)
        {
            squads.Add(squadProduct);
        }

        List<SelectListItem> items = squads.Select(s => new SelectListItem
        {
            Text = s.id.ToString(),
            Value = s.id.ToString()
        }).ToList();

        ViewBag.Squads = items;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RockstarViewModel rockstar)
    {
        if (ModelState.IsValid)
        {
            //TODO Rockstar in database
            return RedirectToAction("Index", "Rockstar");
        }
        else
        {
            List<SquadViewModel> squads = new List<SquadViewModel>();

            var squadProducts = await _squadService.Find();

            foreach (var squadProduct in squadProducts)
            {
                squads.Add(squadProduct);
            }

            List<SelectListItem> items = squads.Select(s => new SelectListItem
            {
                Text = s.id.ToString(),
                Value = s.id.ToString()
            }).ToList();

            ViewBag.Squads = items;
            return View(rockstar);

        }
    }
}