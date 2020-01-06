using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationIsk.Data;
using WebApplicationIsk.Models;

namespace WebApplicationIsk.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserContext _context;

        public AuthorizationController(UserContext context)
        {
            _context = context;
        }

        // GET: Authorization
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        public bool isUserEmpty()
        {
            if(_context.User.Count() == 0)
            {
                return true;
            }
            return false;
        }

        public bool isAuthorized()
        {
            var session_login = HttpContext.Session.Get<string>("Login");
            if (null != session_login && session_login.Length > 0)
            {
                var authorization = _context.User.FirstOrDefault(x => x.Email == session_login || x.Login == session_login);
                if (null != authorization)
                {
                    return true;
                }
            }
            return false;
        }

        public void UnAuthorization()
        {
            if(isAuthorized())
            {
                HttpContext.Session.Remove("Login");
            }
        }

        public bool Authorization(string Login, string Password)
        {
            var session_login = HttpContext.Session.Get<string>("Login");
            if (null == session_login || session_login.Length == 0)
            {
                var authorization = _context.User.FirstOrDefault(x => x.Email == Login || x.Login == Login);
                if (null == authorization)
                {
                    ModelState.AddModelError("Login", "Nieprawidłowy login lub hasło");
                    return false;
                }
                if (authorization.Password != HttpContext.Session.PasswordHash(Password))
                {
                    ModelState.AddModelError("Password", "Podane hasło jest nieprawidłowe!");
                    return false;
                }
                HttpContext.Session.Set<string>("Login", authorization.Login.ToString());
            }
            return this.isAuthorized();
        }

        // GET: Authorization/Login
        public IActionResult Login()
        {
            if (isUserEmpty())
            {
                return RedirectToAction("Register", "Users", new { area = "" });
            }
            if (isAuthorized())
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View();
        }

        // POST: Authorization/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Login,Password")] User user)
        {
            if (isUserEmpty())
            {
                await _context.DisposeAsync();
                return RedirectToAction("Create", "Users", new { area = "" });
            }
            if (Authorization(user.Login, user.Password))
            {
                await _context.DisposeAsync();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (ModelState.IsValid)
            {
                await _context.DisposeAsync();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(user);
        }

        // GET: Authorization/Logout
        public IActionResult Logout()
        {
            UnAuthorization();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}
