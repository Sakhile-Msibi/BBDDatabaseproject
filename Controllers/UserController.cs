using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Website.Models;
using Website.StaticData;

namespace Website.Controllers
{
    public class UserController : Controller
    {
        public static bool sessionState { get; private set;}
        public static string role { get; private set;}
        public static int userId { get; private set;}
        private readonly MyDbContext _db;

        public UserController(MyDbContext db)
        {
            _db = db;
        }
        public IActionResult Register()
        {
            var user = new UserModel();
            return View(user);
        }
        //this is only for testing
        public IActionResult test()
        {
            var val = _db.Users.ToList();
            return View(val);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("UserId", "Email", "FullName", "Password", "Role")]UserModel register)
        {
             if (ModelState.IsValid && !UserModel.EmailExists(register.Email, _db))
            {
                //create
                string hashed_password = SecurePasswordHasherHelper.Hash(register.Password);
                register.Password = hashed_password;

                _db.Users.Add(register);
                _db.SaveChanges();

                return Redirect("Login");
            }

            return View("Register", register);
        }

        public IActionResult RegisterMember()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterMember([Bind("UserId", "Email", "FullName", "Password", "Role")]UserModel register)
        {
            if (/* ModelState.IsValid &&  */!UserModel.EmailExists(register.Email, _db))
            {
                //create
                string hashed_password = SecurePasswordHasherHelper.Hash(register.Password);
                register.Password = hashed_password;

                _db.Users.Add(register);
                _db.SaveChanges();

                return Redirect("../Home/");
            }

            return View(register);
        }

        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
         public IActionResult Login([Bind("UserId", "Email", "Password")]UserModel login)
        {
            if (login.Email == null || login.Password == null) 
                return View();

            string Email = login.Email;
            var user = _db.Users.FirstOrDefault(p => p.Email == Email);

            if (user == null)
                return View("Register");
            
            string userPassword = user.Password;
            
            if(SecurePasswordHasherHelper.Verify(login.Password,userPassword))
            {
                HttpContext.Session.SetInt32("ID", user.UserId);
                sessionState = true;
                role = user.Role;
                userId = user.UserId;
                Console.WriteLine("id" + user.UserId);
                return Redirect("../Home/");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            sessionState = false;
            role = "";
            return Redirect("Login");
        }
        public IActionResult Details(int? id)
        {
            List<UserModel> people = new List<UserModel>();
            people.Add(_db.Users.FirstOrDefault(p => p.UserId == HttpContext.Session.GetInt32("ID")));
            return View(people);
        }
    }
    
}