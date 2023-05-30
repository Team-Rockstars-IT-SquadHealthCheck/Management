using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
            Companies = new List<Company>();
            Users = new List<User>();
        }

        // methods
        public Company TryCreateNewCompany(string name, string address, string telNr)
        {
            Company company = new Company();
            if(NewCompanyMayBeCreated(name, address, telNr))
            {
                company = new Company(name, address, telNr);
                Companies.Add(company);
            }

            return company;
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

        public User TryCreateNewUser(string username)
        {
            User user = new User();

            if (NewUserMayBeCreated(username))
            {
                user = new User(username);
                Users.Add(user);
            }

            return user;
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
