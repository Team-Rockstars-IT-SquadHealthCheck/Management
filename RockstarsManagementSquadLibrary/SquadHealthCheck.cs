using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool TryCreateNewUser()
        {
            bool result = false;

            result = true;

            return result;
        }
    }
}
