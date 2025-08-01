namespace Practice.Model.Response.Auth
{
    public class LoginUserResponse
    {
         public int idUser { get; set; }
        public Guid guidUser { get; set; }
        public int idClient { get; set; }
        public Guid? guidClient { get; set; }
        public int idUserType { get; set; }
        public string userName { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string accessToken { get; set; }
        public DateTime expiration { get; set; }
        public string refreshToken { get; set; }
    }
}
