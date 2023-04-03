using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockstarsManagementSquadLibrary
{
    public class Squad
    {
        // properties
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Survey Survey { get; private set; }
        public List<User> Users { get; private set; }

        // constructors
        public Squad() { }

        public Squad(string name)
        {
            Name = name;
            Users = new List<User>();
        }

        // methods
        public bool AddUserToSquad(User user)
        {
            bool userHasBeenAdded = false;

            if(user != null)
            {
                Users.Add(user);
                userHasBeenAdded = true;
            }

            return userHasBeenAdded;
        }
    }
}
