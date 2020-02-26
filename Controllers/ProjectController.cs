using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Website.Models;
//using Website.StaticData;

namespace Website.Controllers
{
    public class ProjectController : Controller
    {
        private readonly MyDbContext _db;
        private readonly DatabaseModel new_db;

        public ProjectModel Project {get; set;}
        public ProjectController(MyDbContext db)
        {
                _db = db;
                new_db = new DatabaseModel();
        }

        public IActionResult Index()
        {
            if (!(UserController.sessionState))
            {
                return RedirectToAction("Login", "User");
            }
            List<ProjectModel> project = new List<ProjectModel>();
            for(int i = 1; i < 11; i++){
                //project.Add(_db.Projects.FirstOrDefault(p => p.Id == i));
                project.Add(new_db.getProject(i));
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
        public IActionResult Add([Bind("Name","Description", "StartDate", "DueDate")]Models.ProjectModel Project)
        {
            if (!(UserController.sessionState)|| UserController.admin != true)
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                Project.AdminId = UserController.userId;
        
                new_db.addProject(Project);
                //_db.Projects.Add(Project);
                //_db.SaveChanges();
            }
            return View(Project);
        }
    }
}