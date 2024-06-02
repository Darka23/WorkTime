using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkTime.Contracts;
using WorkTime.Data.Identity;
using WorkTime.Data.Models;

namespace WorkTime.Controllers
{
    public class WorkTaskController : BaseController
    {
        private IWorkTaskServices workTaskServices;
        private readonly UserManager<ApplicationUser> userManager;
        public WorkTaskController(IWorkTaskServices _workTaskServices, UserManager<ApplicationUser> _userManager)
        {
            workTaskServices = _workTaskServices;
            userManager = _userManager;
        }
        public IActionResult WorkTaskList()
        {
            var tasks = workTaskServices.GetWorkTasks();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult AddWorkTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkTask([FromForm] WorkTask workTask)
        {
            var currUser = await userManager.GetUserAsync(HttpContext.User);

            await workTaskServices.AddWorkTask(workTask, currUser);

            return RedirectToAction("WorkTaskList", "WorkTask");
        }

        public IActionResult WorkTaskDetails(int id)
        {
            var task = workTaskServices.GetWorkTaskById(id);
            if (task == null)
            {
                return RedirectToAction("WorkTaskList", "WorkTask");
            }
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> WorkOnTask(int id, int workHours)
        {
            var currUser = await userManager.GetUserAsync(HttpContext.User);

            await workTaskServices.WorkOnTask(id, workHours, currUser);

            return RedirectToAction("AddWorkCard", "WorkCard", new { id = id });
        }

        public async Task<IActionResult> RejectTask(int id)
        {
            await workTaskServices.RejectTask(id);

            return RedirectToAction("WorkTaskList", "WorkTask");
        }

        public async Task<IActionResult> DeleteTask(int id)
        {
            await workTaskServices.DeleteTask(id);

            return RedirectToAction("WorkTaskList", "WorkTask");
        }
    }
}
