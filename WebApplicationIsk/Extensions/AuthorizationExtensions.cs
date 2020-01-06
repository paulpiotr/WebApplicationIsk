using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplicationIsk.Data;
using WebApplicationIsk.Models;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace WebApplicationIsk
{
    public static class AuthorizationExtensions
    {
        public static bool IsAuthorizedUser(this ISession session)
        {
            return null != GetAuthorizedUser(session);
        }

        public static User GetAuthorizedUser(this ISession session)
        {
            var context = new UserContext();
            var value = session.GetString("Login");
            if (value != null)
            {
                var login = JsonConvert.DeserializeObject<string>(value);
                var user = context.User.FirstOrDefaultAsync(x => x.Email == login || x.Login == login).Result;
                if (null != user)
                {
                    return user;
                }
            }
            return null;
        }

        public static void RedirectToAuthorization(this ISession session, Controller controller)
        {
            if (false == IsAuthorizedUser(session))
            {
                var url = controller.Url.Link("default", new { controller = "Authorization", action = "Login" });
                controller.Response.Redirect(url);
            }
        }

        public static string PasswordHash(this ISession session, string password)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var salt = Encoding.ASCII.GetBytes(configuration.GetSection("PasswordSalt").GetValue<string>("PasswordSalt"));
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 512 * 100,
                    numBytesRequested: 512 / 8
                )
            );
        }
    }

}
