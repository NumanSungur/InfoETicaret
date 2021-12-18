using Business.Abstract;
using Core.Results.ComplexTypes;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.JwtAuthorizeToken.Abstract;

namespace WebApi.JwtAuthorizeToken.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly string SecretKey;
        private readonly IUsersAdminService users;
        private readonly Microsoft.Extensions.Configuration.IConfiguration config;
        //readonly kullanılan degişken ve interface sadece yapıcı sayesinde türetilebilir.
        public AuthManager( IUsersAdminService _users, Microsoft.Extensions.Configuration.IConfiguration _config)
        {
            config = _config;
            users = _users;
            SecretKey = config["Token:SecurityKey"];            
        }
        public string Authenticate(string username, string password)
        {
            var Data = users.Login(username, password);
            if (Data.ResultStatus== ResultStatus.Success)
            {
                var TokenHandler = new JwtSecurityTokenHandler(); //token üretip, kullanıcıya iletecek sınıfım
                var TokenKey = Encoding.ASCII.GetBytes(SecretKey); //Güvenlik anahtarı appsettings den çekilecek

                //hangi  algoritmeyi  kullanacağı,hangi bilgileri tutacağı ve ne kadar süre geçerli kalacağı 
                var TokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Id", Data.Data.Id.ToString()), //kullancının ıd bilgisi  belirtiliyor.
                        new Claim(ClaimTypes.Name, Data.Data.NameSurname), //Giriş yapanın adı
                        new Claim(ClaimTypes.Role, Data.Data.Roles) // Role Bilgisi
                    }),
                    Expires = DateTime.UtcNow.AddHours(2), //Token 'ın ne kadar kalacağı  belirtiliyor.
                    //hangi key'i ve algoritmayı kullanağı belirtiliyor.
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(TokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                //Yukarıdaki  Discriptor içindeki bilgileri baz alarak algoritmayı üret.
                var Token = TokenHandler.CreateToken(TokenDescriptor);
                //üretilen token ı kullanıcıya gönder.
                return TokenHandler.WriteToken(Token);
            }
            else
            {
                return "Böyle Bir Kullanıcı Bulunamadı";
            }
        }
    }
}
