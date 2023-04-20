using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
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

    [HttpPost]
    public async Task<IActionResult> Create(string username, string password, string email, int roleid, int squadid)
    {
        UserDTO newuserDto = new UserDTO()
        {
            username = username,
            password = "Password1",
            email = email,
            roleid = roleid,
            squadid = squadid,
        };

        try
        {
            var url = await _rockstarsService.Create(newuserDto);
            ViewBag.Message = $"User created at {url}";
        }
        catch (Exception e)
        {
            ViewBag.Error = e.Message;
        }

        return RedirectToAction("Index");
    }
}