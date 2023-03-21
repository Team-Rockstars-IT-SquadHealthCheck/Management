using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;

namespace RockstarsManagementSquad.Controllers;

public class RockstarsController : Controller   
{
    private readonly Services.Interfaces.IRockstarViewModelService _service;

    public RockstarsController(RockstarsManagementSquad.Services.Interfaces.IRockstarViewModelService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public async Task<IActionResult> Index()
    {
        //var squads = await _service.Find();
        var products = await _service.Find();
        // foreach squad create new in view model
        foreach (var product in products)
        {
            RockstarViewModel rockstarViewModel = new RockstarViewModel();
        }
        return View(products);
    }
}