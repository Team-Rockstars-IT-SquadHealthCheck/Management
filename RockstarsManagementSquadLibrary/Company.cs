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
        public Company(int id, string name, string adress, string telNr)
        {
            Id = id;
            Name = name;
            Adress = adress;
            TelNr = telNr;
            Squads = new List<Squad>();
        }

        // methods

    }
}
