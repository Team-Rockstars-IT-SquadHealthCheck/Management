namespace RockstarsManagementSquadLibrary
{
    public class Company
    {
        // properties
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Adress { get; private set; }
        public string TelNr { get; private set; }
        public List<Squad> Squads { get; private set; }

        // constructors
        public Company(string name, string adress, string telNr)
        {
            Name = name;
            Adress = adress;
            TelNr = telNr;
            Squads = new List<Squad>();
        }

        // methods
        public bool CreateNewSquad()
        {
            Squad squad = new Squad();
            Squads.Add(squad);
            bool result = true;

            return result;
        }
    }
}
