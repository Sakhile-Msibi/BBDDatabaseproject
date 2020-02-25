using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Website.Models;
using Website.StaticData;

namespace Website.Controllers
{
    public class ProjectController : Controller
    {
        private readonly MyDbContext _db;

        public ProjectModel Project {get; set;}
        public ProjectController(MyDbContext db)
        {
                _db = db;
        }

        public IActionResult Index()
        {
            if (!(UserController.sessionState))
            {
                return RedirectToAction("Login", "User");
            }
            List<ProjectModel> project = new List<ProjectModel>();
            for(int i = 1; i < 11; i++){
                project.Add(_db.Projects.FirstOrDefault(p => p.Id == i));
            }
            return View("ViewAll", project);
        }

        public IActionResult Add(int? id)
        {
            if (!(UserController.sessionState))
            {
                return RedirectToAction("Login", "User");
            }
            Project = new ProjectModel();
            return View(Project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add()
        {
            Project = new ProjectModel();
            if (!(UserController.sessionState)|| UserController.role != "Admin")
            {
                return RedirectToAction("Login", "User");
            }
         //   Project.AdminId = UserController.userId;
            if (ModelState.IsValid)
            {
                _db.Projects.Add(Project);
                _db.SaveChanges();
            }
            Project = new ProjectModel();
            return View(Project);
        }
    }
}