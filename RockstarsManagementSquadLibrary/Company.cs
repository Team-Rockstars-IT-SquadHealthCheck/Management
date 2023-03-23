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
        public Company() { }

        public Company(string name, string address, string telNr)
        {
            Name = name;
            Address = address;
            TelNr = telNr;
            Squads = new List<Squad>();
        }

        // methods
        public Squad CreateNewSquad(string name, string dbName)
        {
            Squad squad = new Squad();
            if (SquadMayBeCreated(name, dbName))
            {
                squad = new Squad(name);
                Squads.Add(squad);
            }

            return squad;
        }

        private bool SquadMayBeCreated(string name, string dbName)
        {
            bool result = false;

            if (name != string.Empty && name.ToLower() != dbName.ToLower())
            {
                result = true;
            }

            return result;
        }

        public bool AddSquad(Squad squad)
        {
            bool squadHasBeenAdded = false;

            if (squad != null)
            {
                Squads.Add(squad);
                squadHasBeenAdded = true;
            }

            return squadHasBeenAdded;
        }
    }
}
