using Microsoft.AspNetCore.SignalR;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Services.Interfaces;
using RockstarsManagementSquadLibrary;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RockstarsManagementSquad.Services
{
    public class AnswerViewModelService : IAnswerViewModelService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/";

        public AnswerViewModelService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<List<AnswerViewModel>> UserAnswers(int id)
        {
            string path = $"https://localhost:7259/Answer/User/{id}";
            var response = await _client.GetAsync(path); // path was BasePath

            return await response.ReadContentAsync<List<AnswerViewModel>>();
        }
        public async Task<List<AnswerViewModel>> SquadSurveyAnswers(int squadId,int surveyId)
        {
            string path = $"https://localhost:7259/Answer/Squad/{squadId}/Survey/{surveyId}";
            var response = await _client.GetAsync(path); // path was BasePath

            return await response.ReadContentAsync<List<AnswerViewModel>>();
         }

        public async Task<IEnumerable<AnswerViewModel>> GetSquadAnswers(int squadId)
        {
            string path = $"https://localhost:7259/Answer/Squad/{squadId}";
            var response = await _client.GetAsync(path);

            return await response.ReadContentAsync<List<AnswerViewModel>>();
        }

        public async Task<IEnumerable<AnswerViewModel>> GetAllAnswers()
        {
            string path = $"https://localhost:7259/Answers";
            var response = await _client.GetAsync(path);

            return await response.ReadContentAsync<List<AnswerViewModel>>();
        }

        public async Task<IEnumerable<AnswerViewModel>> GetSquadFinnishedEnquetes(int squadId)
        {
            string path = $"https://localhost:7259/Answer/Squad/DistinctAnswer/{squadId}";
            var response = await _client.GetAsync(path);

            return await response.ReadContentAsync<List<AnswerViewModel>>();
        }
    }
}