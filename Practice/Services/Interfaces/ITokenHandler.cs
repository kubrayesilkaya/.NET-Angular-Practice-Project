using Practice.Model.Core;

namespace Practice.Services.Interfaces
{
    public interface ITokenHandler
    {
        public JwtResponseModel CreateAccessToken(int idUser);
    }
}
