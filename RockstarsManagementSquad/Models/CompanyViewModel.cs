namespace RockstarsManagementSquad.Models
{
    public class CompanyViewModel
    {
        
        /// <summary>
        /// The new controller will use the service method we just created and return the data to the frontend.
        /// </summary>
        // properties
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string TelNr { get; private set; }
        public List<SquadViewModel> Squads { get; private set; }
    }
}
