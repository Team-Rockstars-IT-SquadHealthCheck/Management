using System.Net.Http.Headers;
using RockstarsManagementSquad.Models;
using System.Text.Json;
using RockstarsManagementSquad.Models.DTO;

namespace RockstarsManagementSquad;

public static class HttpClientExtensions
    {
        static HttpClient client = new HttpClient();
        
        
        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<T>(
                dataAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result;
        }
    }

