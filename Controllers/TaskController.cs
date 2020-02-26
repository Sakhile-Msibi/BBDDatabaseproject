using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.Controllers
{
    public class TaskController : Controller
    {
        //private readonly MyDbContext _db;
        private readonly DatabaseModel new_db;

        [BindProperty]
        public TaskModel Task { get; set; }
        public TaskController ()
        {
            //_db = db;
            new_db = new DatabaseModel();
        }

        public IActionResult Index(string searchString, string id)
        {
            if (!(UserController.sessionState))
            {
                return RedirectToAction("Login", "User");
            }

//task views go here
            var tasks = new_db.allTasks();
            IEnumerable<TaskModel> filteredTasks = tasks;

            if (!String.IsNullOrEmpty(searchString))
            {
                filteredTasks = tasks.Where(s => s.TaskName.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(id))
            {
                try
                {
                    var convertToNum = Int32.Parse(id);
                    filteredTasks = tasks.Where(s => s.ProjectId.Equals(convertToNum));
                }
                catch
                {

                }
                
            }

            return View(filteredTasks.ToList());
        }

        /*public IActionResult Index()
        {
            List<TaskModel> tasks = new List<TaskModel>();
            for (int i = 1; i < 11; i++)
            {
                tasks.Add(_db.Tasks.FirstOrDefault(p => p.TaskId == i));
            }
            return View(tasks);
        }*/

        public IActionResult Create([Bind("TaskId", "TaskName","TaskDescription", "StartDate", "DueDate", "Progress", "Comments", "ProjectId", "UserId")]Models.TaskModel Task)
        {
            if (!(UserController.sessionState))
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                new_db.addTask(Task);
                //_db.Tasks.Add(Task);
                //_db.SaveChanges();
                return Redirect("Index");
            }
            return View("Create");

        }

        /* public IActionResult Details(int? id)
        {
            List<TaskModel> tasks = new List<TaskModel>();
            if (!id.HasValue)
            {
                for (int i = 1; i < 11; i++)
                {
                    tasks.Add(_db.Tasks.FirstOrDefault(p => p.TaskId == i));
                }
                return View(tasks);
            }

            tasks.Add(_db.Tasks.FirstOrDefault(p => p.TaskId == id.Value));
            return View(tasks);
        } */

        public IActionResult Edit(int? id)
        {
            if (!(UserController.sessionState))
            {
                return RedirectToAction("Login", "User");
            }

            if (id == null)
            {
                return NotFound();
            }

            //var task = await _db.Tasks.FindAsync(id);
            int tid = (int) id;
            var task = new_db.getTask(tid);

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        public IActionResult Delete(int? id)
        {
            if (!(UserController.sessionState))
            {
                return RedirectToAction("Login", "User");
            }
            //if (!UserController.sessionState)
            //return RedirectToAction("User", "Login");
            if (id == null)
            {
                return NotFound();
            }

            int tid = (int) id;
            //var task = await _db.Tasks.FirstOrDefaultAsync(m => m.TaskId == id);
            var task = new_db.getTask(tid);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            //var task = await _db.Tasks.FindAsync(id);
            if (!(UserController.sessionState))
            {
                return RedirectToAction("Login", "User");
            }
            //_db.Tasks.Remove(task);
            new_db.deleteTask(id);
            //await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return new_db.getTask(id) == null ? false : true;
            //return _db.Tasks.Any(e => e.TaskId == id);
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("TaskId", "TaskName","TaskDescription", "StartDate", "DueDate", "Progress", "Comments")] TaskModel task)
        {
            if (!(UserController.sessionState))
            {
                return RedirectToAction("Login", "User");
            }

            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new_db.updateTask(task);
                    //_db.Update(task);
                    //await _db.SaveChangesAsync().ConfigureAwait(false);
                }
                catch
                {
                    if (!TaskExists(task.TaskId))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        

    }
}