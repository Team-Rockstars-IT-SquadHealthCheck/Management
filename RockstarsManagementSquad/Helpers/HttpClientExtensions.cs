using System.Net.Http.Headers;
using RockstarsManagementSquad.Models;
using System.Text.Json;
using RockstarsManagementSquad.Models.DTO;

namespace RockstarsManagementSquad;


/// <summary>
/// Dit stond er op de website:
/// The client will be responsible for direct communication with the API.
/// It will contain a static method responsible for deserializing the API response and formatting it.
/// </summary>

    public static class HttpClientExtensions
    {
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
            //LOCALHOST PORT MOET API PORT ZIJN
            client.BaseAddress = new Uri("https://localhost:7259/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                UserDTO newuserDto = new UserDTO()
                {
                    username = "Gizmo",
                    password = "koekje",
                    email = "emailtest",
                    roleid = 1,
                    squadid = 1,
                };

                var url = await CreateRockstarRecord(newuserDto);
                Console.WriteLine($"Created at {url}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
        
        public static async Task<Uri> CreateRockstarRecord(UserDTO userDto)
        {
            var response = await client.PostAsJsonAsync("api/User", userDto);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
    }

