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

        // GET: Authorization/Login
        public IActionResult Login()
        {
            return View();
        }

        // GET: Authorization/Logout
        public IActionResult Logout()
        {
            return View();
        }

        // POST: Authorization/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Login,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(user);
                //await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

    }
}
