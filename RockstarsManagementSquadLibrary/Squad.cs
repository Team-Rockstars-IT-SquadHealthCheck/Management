using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockstarsManagementSquadLibrary
{
    public class Squad
    {
        public int Id { get; private set; }
        public string SurveyLink { get; private set; }
        public Survey Survey { get; private set; }
        public List<User> Users { get; private set; }
    }
}
