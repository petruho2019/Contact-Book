global using Contacts.Models;
using BCrypt.Net;
using Contacts.Contexts;
using Contacts.Repository.Implementations;
using Contacts.Repository.Interfaces;
using Contacts.Services.Implementations;
using Contacts.Services.Interfaces;
using Contacts.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace Contacts
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    );
            }, ServiceLifetime.Scoped);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            var token = context.Request.Cookies[".AspNetCore.Application.Id"];

                            if (!string.IsNullOrEmpty(token))
                            {
                                context.Token = token;
                            }

                            return Task.CompletedTask;
                        }
                    };
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            builder.Services.AddSingleton<JwtUtils>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IContactBookRepository, ContactBookRepository>();
            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IContactBookService, ContactBookService>();
            builder.Services.AddScoped<IContactService, ContactService>();
            builder.Services.AddScoped<IUserService, UserService>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthorization();
            builder.Services.AddCors();

            var app = builder.Build();

            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();


            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "secureContactBook",
                pattern: "/ContactBook/{*catchall}")
                .RequireAuthorization();

            /*https://localhost:7054/ContactBook/Contacts?contact_book_name=sfsdf*/

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }


    class AuthOptions
    {
        public static readonly string ISSUER = "ContactsProject";
        public static readonly string AUDIENCE = "ContactsUser";
        private static readonly string SIGNINGKEY = "megasuperdupersecretnuiscrutnuykeytoauthentication";

        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(SIGNINGKEY));
    }
}

