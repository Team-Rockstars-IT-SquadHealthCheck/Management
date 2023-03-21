using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Controllers;

public class EnquêtesController : Controller
{
    private readonly Services.Interfaces.ICompanyViewModelService _service;

    public CompaniesController(RockstarsManagementSquad.Services.Interfaces.ICompanyViewModelService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    public IActionResult Index()
    {
        //var companies = await _service.Find();
        var products = await _service.Find();
        // foreach company create new in view model
        foreach (var product in products)
        {
            CompanyViewModel companyViewModel = new CompanyViewModel();
        }
        return View(products);
    }
}