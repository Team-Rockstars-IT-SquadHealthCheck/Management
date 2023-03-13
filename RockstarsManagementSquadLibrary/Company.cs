namespace RockstarsManagementSquadLibrary
{
    public class Company
    {
        // properties
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string TelNr { get; private set; }
        public List<Squad> Squads { get; private set; }

        // constructors
        public Company(string name, string address, string telNr)
        {
            Name = name;
            Address = address;
            TelNr = telNr;
            Squads = new List<Squad>();
        }

        // methods
        public bool CreateNewSquad(string name)
        {
            Squad squad = new Squad(name);
            Squads.Add(squad);
            bool result = true;

            return result;
        }
    }
}
