namespace Practice.Model.Core
{
    public class TokenConfig
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryMinutes { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }
}
