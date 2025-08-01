using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Practice.Extensions;
using Practice.Model.Core;
using Practice.Model.DBContext;
using Practice.Model.Entities;
using Practice.Model.Request.Auth;
using Practice.Model.Response.Auth;
using Practice.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Practice.Model.Enum.GlobalEnum;

namespace Practice.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ITokenHandler tokenHandler;
        private readonly TokenConfig tokenConfig;

        public AuthService(
            ApplicationDbContext dbContext,
            ITokenHandler tokenHandler,
            TokenConfig token
            )
        {
            this.dbContext = dbContext;
            this.tokenHandler = tokenHandler;
            this.tokenConfig = tokenConfig;
        }
        #region Hashing
        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        #endregion


        public async Task UpdateRefreshToken(string refreshToken, int idUser, DateTime accessTokenDate)
        {

            var user = dbContext.Set<Users>().Where(x => x.IdUser == idUser).FirstOrDefault();

            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddMinutes(tokenConfig.RefreshTokenExpiration);
                dbContext.Set<Users>().Update(user);
                dbContext.SaveChanges();
            }
        }

        public async Task<ResponseModel<LoginUserResponse>> Login(LoginUserRequest loginUserRequest)
        {

            ResponseModel<LoginUserResponse> serviceResponse = new();
            LoginUserResponse userLogin = new();

            var userInfo = await dbContext.Set<Users>().FirstOrDefaultAsync(x => x.Deleted == false && (x.UserName == loginUserRequest.userNameOrEmail || x.Email == loginUserRequest.userNameOrEmail));

            if (userInfo == null)
                throw new ValidationException("Hatalı kullanıcı adı ve/veya e-mail girdiniz. Lütfen tekrar deneyiniz.");

            if (userInfo.IsActive == false)
            {
                throw new ValidationException("Kullanıcınız pasif olduğundan giriş yapmanız engellenmiştir.");
            }

            if (!VerifyPasswordHash(loginUserRequest.password, userInfo.PasswordHash, userInfo.PasswordSalt))
            {
                serviceResponse.Result = false;
                serviceResponse.Message = "Hatalı Şifre Girdiniz!";
                return serviceResponse;
            }

            var client = await dbContext.Set<Client>().FirstOrDefaultAsync(x => x.IdClient == userInfo.IdClient);   

            userLogin.idUser = userInfo.IdUser;
            userLogin.guidUser = userInfo.GuidUser;
            userLogin.idClient = client == null ? 0 : client.IdClient;
            userLogin.guidClient = client == null ? null : client.GuidClient;
            userLogin.idUserType = userInfo.IdUserType;
            userLogin.userName = userInfo.UserName;
            userLogin.email = userInfo.Email;
            userLogin.name = userInfo.Name;
            userLogin.surname = userInfo.Surname;

            //Token üretiliyor.
            var token = tokenHandler.CreateAccessToken(userInfo.IdUser);

            userInfo.RefreshToken = token.refreshToken;
            userInfo.RefreshTokenEndDate = token.expiration.AddMinutes(3);

            userLogin.accessToken = token.accessToken;
            userLogin.refreshToken = token.refreshToken;
            userLogin.expiration = token.expiration;

            serviceResponse.Result = true;
            serviceResponse.Message = "Giriş Başarılı";
            serviceResponse.Data = userLogin;
            return serviceResponse;
        }

        
        public async Task<ResponseModel<bool>> AddUser(UserRequestModel userRequestModel)
        {
            ResponseModel<bool> serviceResponse = new ResponseModel<bool>();

            var isUser = await dbContext.Set<Users>().FirstOrDefaultAsync(x => x.Deleted == false && (x.UserName == userRequestModel.userName || x.Email == userRequestModel.email));

            if (isUser != null)
            {
                string userNameMessage = "Bu kullanıcı adı ile başka bir kullanıcı bulunmaktadır. Lütfen başka bir kullanıcı adı kullanınız.";
                string emailMessage = "Bu e-mail ile mevcut bir kullanıcı bulunmaktadır. Lütfen başka bir e-mail kullanınız. ";
                serviceResponse.Result = false;
                serviceResponse.Message = isUser.UserName == userRequestModel.userName ? userNameMessage : emailMessage;
                return serviceResponse;
            }
            else
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userRequestModel.password, out passwordHash, out passwordSalt);

                var user = new Users
                {
                    GuidUser = Guid.NewGuid(),
                    Name = userRequestModel.name,
                    Surname = userRequestModel.surname,
                    Email = userRequestModel.email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    PhoneNumber = userRequestModel.userPhone,
                    IdUserType = (int)UserType.client,
                    IsActive = true,
                    CreDate = DateTime.Now.TrDate(),
                    //CreUser = sessionHelper.GetUserViewModel().idUser,
                    Deleted = false
                };

                dbContext.Set<Users>().Add(user);
                dbContext.SaveChanges();

                serviceResponse.Result = true;
                serviceResponse.Message = "Kullanıcı başarıyla oluşturulmuştur.";
                serviceResponse.Data = true;
                return serviceResponse;
            }
        }

    }
}
