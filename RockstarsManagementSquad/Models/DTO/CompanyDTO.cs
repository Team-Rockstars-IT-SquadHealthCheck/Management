namespace RockstarsManagementSquad.Models.DTO
{
    public class CompanyDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string adress { get; set; }
        public string telephonenr { get; set; }

        public CompanyDTO(int _id, string _name, string _adress, string _telephonenr) 
        { 
            id = _id;
            name = _name;
            adress = _adress;
            telephonenr = _telephonenr;
        }
    }
}
