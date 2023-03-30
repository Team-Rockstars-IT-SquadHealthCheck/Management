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
            string path = $"https://localhost:6001/User/{userId}/Url";
            var response = await _client.PutAsJsonAsync<string>(path, SurveyLink); // path was BasePath

            return await response.ReadContentAsync<string>();
        }
    }
}
