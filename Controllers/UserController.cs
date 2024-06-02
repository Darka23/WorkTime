using Microsoft.AspNetCore.Mvc;
using WorkTime.Contracts;
using WorkTime.Data.Identity;
using WorkTime.Data.Models;

namespace WorkTime.Controllers
{
    public class UserController : BaseController
    {
        private IUserServices userServices;
        private IWorkTaskServices workTaskServices;
        public UserController(IUserServices _userServices, IWorkTaskServices _workTaskServices)
        {
            userServices = _userServices;
            workTaskServices = _workTaskServices;
        }

        public IActionResult UserList()
        {
            var users = userServices.GetUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult EditUser(string id)
        {
            if (id == null)
            {
                return RedirectToAction("WorkTaskList", "WorkTask");
            }
            var currUser = userServices.PlaceholderUser(id);
            return View(currUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(ApplicationUser model)
        {   
            if (await userServices.EditUser(model))
            {
                return RedirectToAction("UserProfile", "User", new {id=model.Id});
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> UserProfile(string id)
        {
            if (id == null)
            {
                return RedirectToAction("WorkTaskList", "WorkTask");
            }
            var user = await userServices.UserProfileInfo(id);
            return View(user);
        }
    }
}
