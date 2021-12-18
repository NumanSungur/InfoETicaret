using Business.AutoMapper.Profiles;
using Business.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace UIAdmin
{
    public class Startup
    {       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddFluentValidation();
            services.MyService();
            services.AddAutoMapper(typeof(AllProfile));
            //kontrol sonrasý nasýl iþlem yapýlacaðýný belirten kod bloðum
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.LoginPath = "/Login"; // giriþ için yönlendirilen kýsým
                x.LogoutPath = "/Login"; // çýkýþ için yönlendirilen kýsým
                x.ExpireTimeSpan = TimeSpan.FromHours(1); //cookie nin kalacagý zaman 
                x.SlidingExpiration = true; //sonlandýrma
                x.AccessDeniedPath = "/Denied";
            });
            //Bütün controller da kontolü yapan yapýmýzdýr.
            services.AddControllersWithViews(x =>
            {
                var dogrulama= new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                x.Filters.Add(new AuthorizeFilter(dogrulama));                
            });
        }      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles(); // wwwroot içerisinde dosyalarý link yoluyla kullanabilme imkaný saglýyor
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();                
            });
        }
    }
}
