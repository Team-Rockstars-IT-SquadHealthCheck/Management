using RockstarsManagementSquad.Models.DTO;
using RockstarsManagementSquad.Models;
using RockstarsManagementSquad.Services.Interfaces;

namespace RockstarsManagementSquad.Services
{
    public class UserViewModelService : IUserViewModelService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/";

        public UserViewModelService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> AddSurveyLinkToUser(string SurveyLink, int userId)
        {
            UrlDTO urlDTO = new UrlDTO(SurveyLink, userId);
            string path = $"https://localhost:7259/Url";
            var response = await _client.PostAsJsonAsync<UrlDTO>(path, urlDTO); // path was BasePath

            return await response.ReadContentAsync<string>();
        }
    }
}
