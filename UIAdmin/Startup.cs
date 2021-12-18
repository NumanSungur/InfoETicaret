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
            //kontrol sonras� nas�l i�lem yap�laca��n� belirten kod blo�um
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.LoginPath = "/Login"; // giri� i�in y�nlendirilen k�s�m
                x.LogoutPath = "/Login"; // ��k�� i�in y�nlendirilen k�s�m
                x.ExpireTimeSpan = TimeSpan.FromHours(1); //cookie nin kalacag� zaman 
                x.SlidingExpiration = true; //sonland�rma
                x.AccessDeniedPath = "/Denied";
            });
            //B�t�n controller da kontol� yapan yap�m�zd�r.
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
            app.UseStaticFiles(); // wwwroot i�erisinde dosyalar� link yoluyla kullanabilme imkan� sagl�yor
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
