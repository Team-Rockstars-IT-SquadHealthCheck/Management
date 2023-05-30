using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RockstarsManagementSquad.Configuration;
using RockstarsManagementSquad.Helpers;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;
using PostmarkDotNet;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Linq;

namespace RockstarsManagementSquad.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ISurveyViewModelService _surveyService;
        private readonly IUserViewModelService _userService;
        private readonly ISquadViewModelService _squadService;
        private readonly IMailService _mailService;

        public SurveysController(ISurveyViewModelService surveyService, IUserViewModelService userService, ISquadViewModelService squadService, IMailService mailService)
        {
            _surveyService = surveyService ?? throw new ArgumentNullException(nameof(surveyService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _squadService = squadService ?? throw new ArgumentNullException(nameof(squadService));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }
        
        public async Task<IActionResult> Index()
        {
            var squads = await _squadService.FindAll();
            var products = await _surveyService.Find();

            List<SelectListItem> items = squads.Select(c => new SelectListItem
            {
                Text = c.name.ToString(),
                Value = c.id.ToString()
            }).ToList();

            ViewBag.Squads = items;

            List<SurveyViewModel> surveyViewModels = new List<SurveyViewModel>();

            foreach (var product in products)
            {
                SurveyViewModel surveyViewModel = product.ConvertSurveyDTOToSurveyViewModel();
                surveyViewModels.Add(surveyViewModel);
            }

            List<SelectListItem> surveys = products.Select(c => new SelectListItem
            {
                Text = c.name.ToString(),
                Value = c.id.ToString()
            }).ToList();

            ViewBag.Surveys = surveys;

            return View(surveyViewModels);
        }

        public async Task<IActionResult> CreateSurvey(string title, string description)
        {
            Survey survey = new Survey(title, description);
            CreateNewSurvey(survey);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SendSurveyLink(int surveyId, int squadId)
        {
            _surveyService.CreateLinkSurveySquad(new LinkSurveySquadDTO(surveyId, squadId));

            IEnumerable<RockstarViewModel> UsersInSquad = await _squadService.UsersInSquad(squadId);
            List<RockstarViewModel> usersList = UsersInSquad.ToList();

            Survey survey = _surveyService.FindById(surveyId).Result.ConvertSurveyDTOToSurvey();

            foreach (var user in usersList)
            {
                int userId = user.id;
                string surveyLink = survey.CreateNewSurveyLink(surveyId, squadId, userId);
                
                _userService.AddSurveyLinkToUser(surveyLink, userId);

                await SendMail(
                    new MailData() 
                    { 
                        EmailToAddress = user.email,
                        EmailToName = user.username,
                        EmailSubject = "Squad Health Check",
                        EmailBody = $"This is your personal surveylink: http://138.201.52.251:82/Home/Index/{surveyLink}"
                    }
                    );
            }
            return RedirectToAction("Index");
        }

        private Survey CreateNewSurvey(Survey survey)
        {
            _surveyService.Create(survey.ConvertSurveyToSurveyDTO());

            return survey;
        }

        [HttpPost]
        [Route("SendMail")]
        public async Task<bool> SendMail(MailData mailData)
        {
            return await _mailService.SendMail(mailData);
        }
    }
}
