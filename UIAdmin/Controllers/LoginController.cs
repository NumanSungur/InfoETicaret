using Business.Abstract;
using Core.Results.ComplexTypes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UIAdmin.Controllers
{
    [AllowAnonymous] // Authorization Özelliğinin Bu sayfayı kontrol etmemesini saglar.
    public class LoginController : Controller
    {
        private readonly IUsersAdminService db;
        public LoginController(IUsersAdminService _db)
        {
            db = _db;   
        }
        public async Task<IActionResult> Index()
        {
            // .Net Core Identity => Üyelik Sistemlerini,Login İşlemlerini daha detaylı ve daha esnek sağlayan bir kütüphanedir.
            //Identity => veritabanlıda kullanılabilir. veritabansızda kullanılabilir.
            //Role Table
            //UserLogin ,UserRoles , UserToken

            //Authentication =>Kullanıcın sistem var olup olmadığını sorgulayan ve bu duruma göre işlemler yapmamızı sağlayan yapıdır.
            //Authorization =>Sisteme Giriş yapan kişinin yetkisine göre girebileceği yerleri belirleyen yapıdır.
            //Claims => Sisteme Giriş yapan kullanıcın bilgilerini tutan yapıdır.(Role,AdSoyad,Email vs)
            //Role => Sisteme giriş kullanıcının hangi rol yapısında olduğu ve rolüne göre kontrol sağlayan yapıdır.
            //Third Party Authentication => Üçüncü taraf kimlik doğrulama. Facebook , Google ile giriş yapılması istenildiğinde

            // var data = db.GetAll().Data;

            //var claimtypesname = user.ıdentity.name;
            //var claimtypesrole = user.ısınrole("admin");
            //var customıd = user.claims.firstordefault(x => x.type == "ıd").value.tostring();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string Email,string Password)
        {
            //Asenkron Programlama => işn parçalara ayrılıp tüm işlemlerin  aynı anda sürdürülmesini sağlayan yapıdır.

            if (db.Login(Email,Password).ResultStatus==ResultStatus.Success)
            {
                var data = db.Login(Email, Password).Data;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,data.NameSurname),//Varsayılan yapıda çagırmak kolay
                    new Claim(ClaimTypes.Role,data.Roles), //verileri saklanırken şifrelicez daha sonra. Sha1,Md5
                    new Claim("Id",data.Id.ToString()) //Custom yapıda tanımlamak zordur.
                };
                var UserIdentity = new ClaimsIdentity(claims, "LoginControl"); //Talep i hazırlıyoruz.
                ClaimsPrincipal principal = new ClaimsPrincipal(UserIdentity); //Talep sorumlusu, yani hazır hale geliyor.
                await HttpContext.SignInAsync(principal);
                return Redirect("/Home");
            }
            else
            {
                return View();
            }            
        }       
    }
}
