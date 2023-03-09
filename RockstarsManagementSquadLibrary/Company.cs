namespace RockstarsManagementSquadLibrary
{
    public class Company
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Adress { get; private set; }
        public string TelNr { get; private set; }
        public List<Squad> Squads { get; private set; }
    }
}
