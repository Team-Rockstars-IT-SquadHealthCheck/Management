using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Controllers;

public class CompaniesController : Controller
{
    private readonly ICompanyViewModelService _companyService;
    private readonly ISquadViewModelService _squadService;

    public CompaniesController(ICompanyViewModelService companyService, ISquadViewModelService squadService)
    {
        _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        _squadService = squadService ?? throw new ArgumentNullException(nameof(squadService));
    }
    
    public async Task<IActionResult> Index()
    {
        //var companies = await _service.Find();
        var companies = await _companyService.Find();
        var squads = await _squadService.FindAll();;

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

        ViewBag.Companies = companyItems;
        ViewBag.Squads = SquadItems;
        // foreach company create new in view model
        foreach (var company in companies)
        {
            CompanyViewModel companyViewModel = new CompanyViewModel();
        }
        return View(companies);
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

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _companyService.Delete(id);
        }
        catch (Exception e)
        {
            ViewBag.Error = e.Message;
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Add(int squadId, int companyId)
    {
        _squadService.AddSquadToCompany(squadId, companyId);
        return RedirectToAction("Index");
    }
}
