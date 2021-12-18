using Business.Abstract;
using Core.Results.ComplexTypes;
using Entities.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UIWeb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomersService db;
        public CustomerController(ICustomersService _db)
        {
            db = _db; 
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(string KullaniciAdi,string KullaniciSifre)
        {
            bool sonuc = (KullaniciAdi.Contains("@") ? true : false);
            if (sonuc) // true ise mail olarak algılayacak
            {
                if (db.Login(KullaniciAdi, null, KullaniciSifre).ResultStatus == ResultStatus.Success)
                {
                    var data = db.Login(KullaniciAdi, null, KullaniciSifre);
                    var claims = new List<Claim>
                    {
                    new Claim("ID",data.Data.Id.ToString()),
                    new Claim(ClaimTypes.Name,data.Data.NameSurname),
                    };
                    var UserIdentity = new ClaimsIdentity(claims, "LoginGiris");
                    ClaimsPrincipal principal = new ClaimsPrincipal(UserIdentity);
                    var CookiesSüresi = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(120), // Kaç Dakika Kalacağı
                        IsPersistent = true                         // Tarayıcı Kapanıp açıldığında Duracak mı ?
                    };
                    await HttpContext.SignInAsync(principal, CookiesSüresi);
                    return Redirect("/");
                }
                else
                {
                    TempData["Error"] = "Giriş Başarısız, Lütfen Tekrar Deneyiniz...";
                    return View();
                }
            }
            else // telefon olarak
            {
                if (db.Login(null, KullaniciAdi, KullaniciSifre).ResultStatus == ResultStatus.Success)
                {
                    var data = db.Login(null, KullaniciAdi, KullaniciSifre);
                    var claims = new List<Claim>
                    {
                    new Claim("ID",data.Data.Id.ToString()),
                    new Claim(ClaimTypes.Name,data.Data.NameSurname),
                    };
                    var UserIdentity = new ClaimsIdentity(claims, "LoginGiris");
                    ClaimsPrincipal principal = new ClaimsPrincipal(UserIdentity);
                    var CookiesSüresi = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(120), // Kaç Dakika Kalacağı
                        IsPersistent = true                         // Tarayıcı Kapanıp açıldığında Duracak mı ?
                    };
                    await HttpContext.SignInAsync(principal, CookiesSüresi);
                    return Redirect("/");
                }
                else
                {
                    TempData["Error"] = "Giriş Başarısız, Lütfen Tekrar Deneyiniz...";
                    return View();
                }
            }            
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(CustomersUpdateDto data)
        {
            if (Request.Form["Advert"] == "on")
            {
                data.Advert = true;
            }
            data.Gender = true;
            if (db.Add(data).ResultStatus == ResultStatus.Success)
            {
                CustomersDto uye = db.Login(data.Email, data.Phone, data.Password).Data;
                var claims = new List<Claim>
                { 
                    new Claim("ID",uye.Id.ToString()),
                    new Claim(ClaimTypes.Name,uye.NameSurname),
                };
                var UserIdentity = new ClaimsIdentity(claims, "LoginGiris");
                ClaimsPrincipal principal = new ClaimsPrincipal(UserIdentity);
                var CookiesSüresi = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(120), // Kaç Dakika Kalacağı
                    IsPersistent = true                         // Tarayıcı Kapanıp açıldığında Duracak mı ?
                };
                await HttpContext.SignInAsync(principal, CookiesSüresi);
                return Redirect("/");
            }
            else
            {
                TempData["Error"] = "Kayıt Başarısız, Lütfen Tekrar Deneyiniz...";
                return View("Index");
            }
            
        }
        public IActionResult Hesabim()
        {
            return View();
        }
        public IActionResult Siparişlerim()
        {
            return View();
        }
    }
}
