namespace Practice.Model.Request.Auth
{
    public class LoginUserRequest
    {
        public string userNameOrEmail { get; set; }
        public string password { get; set; }
        //public string deviceToken { get; set; }
    }
}
