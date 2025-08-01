using Practice.Services.Interfaces;

namespace Practice.Services
{
    public class SessionHelper : ISessionHelper
    {
        private readonly IHttpContextAccessor httpContextAccessor;
    }
}
