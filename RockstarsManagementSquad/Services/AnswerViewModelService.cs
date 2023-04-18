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

        public async Task<IEnumerable<AnswerViewModel>> UserAnswers(int id)
        {
            string path = "https://localhost:7259/companies";
            var response = await _client.GetAsync(path); // path was BasePath

            return await response.ReadContentAsync<List<AnswerViewModel>>();
        }
    }
}
