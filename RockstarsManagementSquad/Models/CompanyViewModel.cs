namespace RockstarsManagementSquad.Models
{
    public class CompanyViewModel
    {
        
        /// <summary>
        /// The new controller will use the service method we just created and return the data to the frontend.
        /// </summary>
        // properties
        public int id { get; set; }
        public string name { get; set; }
        public string adress { get; set; }
        public string telephonenr { get; set; }
        public List<SquadViewModel> squads { get; set; }
    }
}
