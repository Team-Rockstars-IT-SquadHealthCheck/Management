using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RockstarsManagementSquadLibrary
{
    public class SquadHealthCheck
    {
        // properties
        public List<Company> Companies { get; private set; }
        public List<User> Users { get; private set; }

        // constructors
        public SquadHealthCheck()
        {

        }

        // methods
        public bool TryCreateNewCompany(string name, string adress, string telNr)
        {
            bool result = false;

            if(NewCompanyMayBeCreated(name, adress, telNr))
            {
                result = true;
            }

            return result;
        }

        private bool NewCompanyMayBeCreated(string name, string adress, string telNr)
        {
            bool result = false;

            if (name != string.Empty && adress != string.Empty && telNr != string.Empty)
            {
                result = true;
            }

            return result;
        }

        public bool TryCreateNewUser(string username)
        {
            bool result = false;

            if (NewUserMayBeCreated(username))
            {
                User user = new User(username);
                result = true;
            }

            return result;
        }

        private bool NewUserMayBeCreated(string username)
        {
            bool result = false;

            if (username != string.Empty)
            {
                result = true;
            }

            return result;
        }
    }
}
