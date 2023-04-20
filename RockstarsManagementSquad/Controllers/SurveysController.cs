using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RockstarsManagementSquad.Configuration;
using RockstarsManagementSquad.Helpers;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;
using PostmarkDotNet;

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
            var products = await _surveyService.Find();
            foreach (var product in products)
            {
                SurveyViewModel surveyViewModel = new SurveyViewModel();
            }
            return View(products);
        }

        public async Task<IActionResult> CreateSurveyLink(int squadId)
        {
            IEnumerable<RockstarViewModel> UsersInSquad = await _squadService.UsersInSquad(squadId);
            List<RockstarViewModel> usersList = UsersInSquad.ToList();
            
            int surveyNumber = 0;

            Survey survey = new Survey(); Random rnd = new Random();

            for (int j = 0; j < 1; j++)
            {
                surveyNumber = rnd.Next(0, 9999);
            }

            foreach (var user in usersList)
            {
                int userId = user.id;
                string surveyLink = survey.CreateNewSurveyLink(surveyNumber, squadId, userId);
                _userService.AddSurveyLinkToUser(surveyLink, userId);
                await SendMail(
                    new MailData() 
                    { 
                        EmailToAddress = user.email,
                        EmailToName = user.username,
                        EmailSubject = "Squad Health Check",
                        EmailBody = $"This is your personal surveylink: {surveyLink}"
                    }
                    );
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("SendMail")]
        public async Task<bool> SendMail(MailData mailData)
        {
            return await _mailService.SendMail(mailData);
        }
    }
}
