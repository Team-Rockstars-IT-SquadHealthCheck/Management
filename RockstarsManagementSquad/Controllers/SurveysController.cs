using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;

namespace RockstarsManagementSquad.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ISurveyViewModelService _surveyService;
        private readonly IUserViewModelService _userService;
        private readonly ISquadViewModelService _squadService;

        public SurveysController(ISurveyViewModelService surveyService, IUserViewModelService userService, ISquadViewModelService squadService)
        {
            _surveyService = surveyService ?? throw new ArgumentNullException(nameof(surveyService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _squadService = squadService ?? throw new ArgumentNullException(nameof(squadService));
        }

        public async Task<IActionResult> Index()
        {
            var squads = await _squadService.Find();
            var products = await _surveyService.Find();

            List<SelectListItem> items = squads.Select(c => new SelectListItem
            {
                Text = c.name.ToString(),
                Value = c.id.ToString()
            }).ToList();

            ViewBag.Squads = items;

            foreach (var product in products)
            {
                SurveyViewModel surveyViewModel = new SurveyViewModel();
            }
            return View(products);
        }

        public async Task<IActionResult> CreateSurveyLink(int squadId)
        {
            IEnumerable<UserViewModel> UsersInSquad = await _squadService.UsersInSquad(squadId);
            List<UserViewModel> usersList = UsersInSquad.ToList();
            
            int surveyNumber = 0;

            Survey survey = new Survey(); Random rnd = new Random();

            for (int j = 0; j < 1; j++)
            {
                surveyNumber = rnd.Next(0, 9999);
            }

            foreach (var user in usersList)
            {
                int userId = user.Id;
                string surveyLink = survey.CreateNewSurveyLink(surveyNumber, squadId, userId);
                _userService.AddSurveyLinkToUser(surveyLink, userId);
            }
            return RedirectToAction("Index");
        }
    }
}
