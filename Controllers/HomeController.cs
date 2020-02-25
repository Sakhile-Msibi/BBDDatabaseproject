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
    public class HomeController : Controller
    {
        
        private readonly MyDbContext _db;
        private readonly DatabaseModel new_db;

        public HomeController(MyDbContext db)
        {
            _db = db;
            new_db = new DatabaseModel();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult UseDefaults()
        {
            List<UserModel> defaultUser = UserData.People;
            foreach(UserModel Dude in defaultUser) {
                _db.Users.Add(Dude);
            }
            _db.SaveChanges();
            List<TaskModel> defaultTask = TaskData.People;
            foreach(TaskModel Dude in defaultTask) {
                _db.Tasks.Add(Dude);
            }
            _db.SaveChanges();
            List<ProjectModel> defaultProject = ProjectData.People;
            foreach(ProjectModel Dude in defaultProject) {
                _db.Projects.Add(Dude);
            }
            _db.SaveChanges();
            return View();
        }
    }
}
