using RockstarsManagementSquad.Models;
using System.Text.Json;

namespace RockstarsManagementSquad;


/// <summary>
/// Dit stond er op de website:
/// The client will be responsible for direct communication with the API.
/// It will contain a static method responsible for deserializing the API response and formatting it.
///
/// chatGPT:
/// This code defines an extension method to read and deserialize the content of a successful HTTP response into an object of a specified type using System.Text.Json.
/// It checks if the response was successful and throws an exception if not. The method is asynchronous,
/// reads the response content as a string, and deserializes it into an object of type T with the PropertyNameCaseInsensitive option set to true.
/// This simplifies HTTP request/response handling in .NET applications.
/// </summary>

    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonSerializer.Deserialize<T>(
                dataAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return result;
        }
    }
