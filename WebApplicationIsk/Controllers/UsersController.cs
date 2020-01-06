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
    public class UsersController : Controller
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        public bool EmailExist(string Email, Int64 Id)
        {
            var validate = _context.User.FirstOrDefault(x => x.Email == Email && x.Id != Id);
            if (validate != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EmailExist(string Email)
        {
            var validate = _context.User.FirstOrDefault(x => x.Email == Email);
            if (validate != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LoginExist(string Login, Int64 Id)
        {
            var validate = _context.User.FirstOrDefault(x => x.Login == Login && x.Id != Id);
            if (validate != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LoginExist(string Login)
        {
            var validate = _context.User.FirstOrDefault(x => x.Login == Login);
            if (validate != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.RedirectToAuthorization(this);
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpContext.Session.RedirectToAuthorization(this);
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Email,Login,Password,Created")] User user)
        {
            if (this.EmailExist(user.Email))
            {
                ModelState.AddModelError("Email", "Email " + user.Email + " jest już używany!");
            }
            if (this.LoginExist(user.Login))
            {
                ModelState.AddModelError("Login", "Login " + user.Login + " jest już używany!");
            }
            if (ModelState.IsValid)
            {
                user.Password = HttpContext.Session.PasswordHash(user.Password);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            HttpContext.Session.RedirectToAuthorization(this);
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Email,Role,Login,Password,Created")] User user)
        {
            HttpContext.Session.RedirectToAuthorization(this);
            if (this.EmailExist(user.Email))
            {
                ModelState.AddModelError("Email", "Email " + user.Email + " jest już używany!");
            }
            if (this.LoginExist(user.Login))
            {
                ModelState.AddModelError("Login", "Login " + user.Login + " jest już używany!");
            }
            if (ModelState.IsValid)
            {
                user.Password = HttpContext.Session.PasswordHash(user.Password);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            HttpContext.Session.RedirectToAuthorization(this);
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Email,Role,Login,Password,Created")] User user)
        {
            HttpContext.Session.RedirectToAuthorization(this);
            if (id != user.Id)
            {
                return NotFound();
            }
            if (this.EmailExist(user.Email, user.Id))
            {
                ModelState.AddModelError("Email", "Email " + user.Email + " jest już używany!");
            }
            if (this.LoginExist(user.Login, user.Id))
            {
                ModelState.AddModelError("Login", "Login " + user.Login + " jest już używany!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    user.Password = HttpContext.Session.PasswordHash(user.Password);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            HttpContext.Session.RedirectToAuthorization(this);
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpContext.Session.RedirectToAuthorization(this);
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
