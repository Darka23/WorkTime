using Microsoft.AspNetCore.Mvc;
using WorkTime.Data.Identity;
using WorkTime.Data.Models;

namespace WorkTime.Contracts
{
    public interface IWorkTaskServices
    {
        Task AddWorkTask([FromForm] WorkTask workTask, ApplicationUser user);
        IEnumerable<WorkTask> GetWorkTasks();
        WorkTask GetWorkTaskById(int id);
        Task WorkOnTask(int id, int workHours, ApplicationUser user);
        Task RejectTask(int id);
        Task DeleteTask(int id);
    }
}
