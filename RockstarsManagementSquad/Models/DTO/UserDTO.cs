namespace RockstarsManagementSquad.Models.DTO
{
    public class UserDTO
    {
        public int id { get;  set; }
        public string username { get;  set; }
        public string password { get;  set; }
        public string email { get;  set; }
        public int roleid { get;  set; }
        public int squadid { get;  set; }

        public UserDTO(int _id, string _username, string _password, string _email, int _roleid, int _squadid)
        {
            id = _id;
            username = _username;
            password = _password;
            email = _email;
            roleid = _roleid;
            squadid = _squadid;
        }
    }
}
