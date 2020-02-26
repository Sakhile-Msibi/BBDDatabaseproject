using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Website.Models;
//using Website.StaticData;

namespace Website.Controllers
{
    public class UserController : Controller
    {
        public static bool sessionState { get; private set;}
        public static bool admin { get; private set;}
        public static int userId { get; private set;}
        //private readonly MyDbContext _db;

        private readonly DatabaseModel new_db;

        public UserController()
        {
            //_db = db;
            new_db = new DatabaseModel();
        }
        public IActionResult Register()
        {
            var user = new UserModel();
            return View(user);
        }
        //this is only for testing
        /* public IActionResult test()
        {
            var val = new_db.GetUsers().ToList();
            return View(val);
        } */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("UserId", "Email", "FullName", "Password", "Admin")]UserModel register)
        {
             if (ModelState.IsValid) //&& !UserModel.EmailExists(register.Email, _db))
            {
                //create
                string hashed_password = SecurePasswordHasherHelper.Hash(register.Password);
                register.Password = hashed_password;

                new_db.addUser(register);
                /* _db.Users.Add(register);
                _db.SaveChanges(); */

                return Redirect("Login");
            }

            return View("Register", register);
        }

        public IActionResult RegisterMember()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterMember([Bind("UserId", "Email", "FullName", "Password", "Admin")]UserModel register)
        {
            if (ModelState.IsValid) //&&  */!UserModel.EmailExists(register.Email, _db))
            {
                //create
                string hashed_password = SecurePasswordHasherHelper.Hash(register.Password);
                register.Password = hashed_password;

                new_db.addUser(register);
                //_db.Users.Add(register);
                //_db.SaveChanges();

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
            //find user in database
            var user = new_db.findUser(Email);
            if (user == null)
                return View("Register");
            string userPassword = user.Password;
            
            if(SecurePasswordHasherHelper.Verify(login.Password,userPassword))
            {
                HttpContext.Session.SetInt32("ID", user.UserId);
                sessionState = true;
                admin = user.Admin;
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
            admin = false;
            return Redirect("Login");
        }
       /*  public IActionResult Details(int? id)
        {
            //nyeh?
            List<UserModel> people = new List<UserModel>();
            people.Add(_db.Users.FirstOrDefault(p => p.UserId == HttpContext.Session.GetInt32("ID")));
            return View(people);
        } */
    }
    
}