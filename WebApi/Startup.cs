using Business.AutoMapper.Profiles;
using Business.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.JwtAuthorizeToken.Abstract;
using WebApi.JwtAuthorizeToken.Concrete;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(x => x.AddPolicy("CorsPolicy", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            services.AddControllers();
            services.MyService();
            services.AddAutoMapper(typeof(AllProfile));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                var JwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Swagger Dogrulama",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Token Bilginizi Aþaðýdaki Metin Kutusuna Yazýnýz.",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(JwtSecurityScheme.Reference.Id, JwtSecurityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {JwtSecurityScheme,Array.Empty<string>()}
                });
            });
            //appsettings içerisinde security key çekiyoruz.
            string SecurityKey = Configuration["Token:SecurityKey"];
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,           //Herkes Kullanabilir.
                    ValidAudience = Configuration["Token:Audience"], //Kullanýlacak Adresler.
                    ValidateIssuer= false,          //Dagýtým yapan bilgisi Açýk
                    ValidIssuer= Configuration["Token:Issuer"],   //Dagýtým yapan
                    ValidateIssuerSigningKey=true,    //Çözmek için güvenlik anahtarý var.
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey))
                };
            });
            services.AddScoped<IAuthService, AuthManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sungur.com");
                    c.DefaultModelsExpandDepth(-1);
                });
                
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
