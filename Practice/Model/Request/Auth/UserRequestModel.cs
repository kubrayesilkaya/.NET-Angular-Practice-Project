namespace Practice.Model.Request.Auth
{
    public class UserRequestModel
    {
        public int idUsers { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string userPhone { get; set; }
        public int? idUserType { get; set; }
        //public string refreshToken { get; set; }
    }
}
