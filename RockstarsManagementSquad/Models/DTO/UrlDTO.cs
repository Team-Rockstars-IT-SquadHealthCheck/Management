namespace RockstarsManagementSquad.Models.DTO
{
    public class UrlDTO
    {
        public string url { get; set; }
        public int userid { get; set; }
        public UrlDTO(string Url, int userId)
        {
            url = Url;
            userid = userId;
        }
    }
}
