using Microsoft.AspNetCore.Mvc;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Controllers;

public class SurveysController : Controller
{
    private readonly Services.Interfaces.ISurveyViewModelService _service;

    public SurveysController(RockstarsManagementSquad.Services.Interfaces.ISurveyViewModelService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    public async Task<IActionResult> Index()
    {
        //var companies = await _service.Find();
        var products = await _service.Find();
        // foreach company create new in view model
        foreach (var product in products)
        {
            SurveyViewModel surveyViewModel = new SurveyViewModel();
        }
        return View(products);
    }
}