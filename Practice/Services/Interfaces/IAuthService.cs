using Practice.Model.Core;
using Practice.Model.Request.Auth;
using Practice.Model.Response.Auth;

namespace Practice.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseModel<LoginUserResponse>> Login(LoginUserRequest loginUserRequest);
        Task<ResponseModel<bool>> AddUser(UserRequestModel userRequestModel);
    }
}
