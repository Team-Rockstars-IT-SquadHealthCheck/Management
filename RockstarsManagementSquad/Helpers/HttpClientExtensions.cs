using System.Net.Http.Headers;
using RockstarsManagementSquad.Models;
using System.Text.Json;
using RockstarsManagementSquad.Models.DTO;

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
        //Dit is voor de tweede method, in het geval dat dit alle opkut, gewoon weghalen, dan vind ik wel een andere manier
        static HttpClient client = new HttpClient();
        
        
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
        
        
        /// <summary>
        /// The following code:
        /// Sets the base URI for HTTP requests.
        /// Sets the Accept header to "application/json". Setting this header tells the server to send data in JSON format.
        /// </summary>
        private static async Task RunAsync()
        {
            try
            {
                //LOCALHOST PORT MOET API PORT ZIJN
                client.BaseAddress = new Uri("http://localhost:7078/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        
        //Ik ben benieuwd of dit gaat werken, ben gewoon blind een artikel van microsoft aan het volgen namelijk
        //UPDATE: het werkt
        public static async Task<Uri> CreateRockstarRecord(UserDTO userdto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/User", userdto);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
    }

