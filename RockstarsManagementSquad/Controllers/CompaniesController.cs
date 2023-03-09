using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Services.Interfaces;

namespace RockstarsManagementSquad.Controllers;

public class CompaniesController : Controller   
{
    private readonly Services.Interfaces.ICompanyViewModelService _service;

    public CompaniesController(RockstarsManagementSquad.Services.Interfaces.ICompanyViewModelService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public async Task<IActionResult> Index()
    {
        var products = await _service.Find();
        return View(products);
    }
}