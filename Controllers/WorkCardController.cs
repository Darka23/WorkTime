using Microsoft.AspNetCore.Mvc;
using WorkTime.Contracts;
using WorkTime.Data.Models;

namespace WorkTime.Controllers
{
    public class WorkCardController : BaseController
    {
        private IWorkCardServices workCardServices;

        public WorkCardController(IWorkCardServices _workCardServices)
        {
            workCardServices = _workCardServices;
        }

        [HttpGet]
        public IActionResult AddWorkCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkCard([FromForm] WorkCard workCard, int id)
        {
            await workCardServices.AddWorkCard(workCard, id);
            return RedirectToAction("WorkTaskList", "WorkTask");
        }
    }
}
