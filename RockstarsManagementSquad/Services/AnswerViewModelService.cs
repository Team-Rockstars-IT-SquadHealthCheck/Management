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
        public async Task<IEnumerable<AnswerViewModel>> SquadAnswers(int id)
        {
            string path = $"https://localhost:7259/Answer/Squad/{id}";
            var response = await _client.GetAsync(path); // path was BasePath

            return await response.ReadContentAsync<List<AnswerViewModel>>();
        }
    }
}