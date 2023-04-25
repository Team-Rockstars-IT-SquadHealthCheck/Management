using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Controllers;

public class CompaniesController : Controller
{
    private readonly Services.Interfaces.ICompanyViewModelService _companyService;

    public CompaniesController(RockstarsManagementSquad.Services.Interfaces.ICompanyViewModelService service)
    {
        _companyService = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    public async Task<IActionResult> Index()
    {
        //var companies = await _service.Find();
        var products = await _companyService.Find();
        // foreach company create new in view model
        foreach (var product in products)
        {
            CompanyViewModel companyViewModel = new CompanyViewModel();
        }
        return View(products);
    }

    Company company = new Company();

    [HttpPost]
    public IActionResult Create(string name, string address, string telNr)
    {
        SquadHealthCheck squadHealthCheck = new SquadHealthCheck();

        company = squadHealthCheck.TryCreateNewCompany(name, address, telNr);
        CompanyDTO cDTO = new CompanyDTO(0, company.Name, company.Address, company.TelNr);

        _companyService.Create(cDTO);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Info(int? id)
    {
        var company = await _companyService.FindById(id);
        var squads = await _companyService.SquadsInCompany(id);
        CompanyInfoViewModel infoViewModel = new CompanyInfoViewModel();
        infoViewModel.company = company;
        infoViewModel.squads = squads;

        return View(infoViewModel);
    }
}
