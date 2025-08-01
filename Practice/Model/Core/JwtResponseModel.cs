namespace Practice.Model.Core
{
    public class JwtResponseModel
    {
        public string accessToken { get; set; }
        public DateTime expiration { get; set; }
        public string refreshToken { get; set; }
    }
}
