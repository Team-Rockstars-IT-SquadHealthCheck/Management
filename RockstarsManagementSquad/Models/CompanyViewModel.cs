namespace RockstarsManagementSquad.Models
{
    public class CompanyViewModel
    {
        // properties
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Adress { get; private set; }
        public string TelNr { get; private set; }
        public List<SquadViewModel> Squads { get; private set; }
    }
}
