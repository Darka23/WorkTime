using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTime.Contracts;
using WorkTime.Data.Identity;
using WorkTime.Data.Models;
using WorkTime.Data.Repositories;

namespace WorkTime.Services
{
    public class WorkTaskServices : IWorkTaskServices
    {
        private IApplicationDbRepository repo;
        public WorkTaskServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }
        public async Task AddWorkTask([FromForm] WorkTask workTask, ApplicationUser user)
        {
            var existing = repo.All<WorkTask>()
               .Where(r => r.Name == workTask.Name)
               .FirstOrDefault();

            if (existing != null)
            {
                throw new ArgumentException("WorkTask already exists");
            }

            await repo.AddAsync(new WorkTask()
            {
                Name = workTask.Name,
                Creator = user,
                CreatorId = user.Id,
                Description = workTask.Description,
                TotalHours = workTask.TotalHours,
                Status = "Нова",
                CurrentUsedHours = 0,
            });

            await repo.SaveChangesAsync();
        }

        public WorkTask GetWorkTaskById(int id)
        {
            return repo.All<WorkTask>()
                .Where(x => x.Id == id)
                .Include(x => x.Creator)
                .Include(x => x.Workers)
                .First();
        }

        public IEnumerable<WorkTask> GetWorkTasks()
        {
            return repo.All<WorkTask>()
                .Include(x => x.Creator)
                .Include(x => x.Workers)
                .ToList();
        }

        public async Task RejectTask(int id)
        {
            var task = this.GetWorkTaskById(id);
            if (task == null)
            {
                throw new ArgumentException("Task does not exist");
            }

            task.Status = "Отказана";
            task.Workers?.Clear();

            await repo.SaveChangesAsync();
        }

        public async Task WorkOnTask(int id, int workHours, ApplicationUser user)
        {
            var task = this.GetWorkTaskById(id);

            if (task == null)
            {
                throw new ArgumentException("Task does not exist");
            }

            task.Status = "Започната";

            task.Workers?.Add(user);
            task.CurrentUsedHours += workHours;

            if (task.StartDate == null)
            {
                task.StartDate = DateTime.Now;
            }

            if (task.TotalHours <= task.CurrentUsedHours)
            {
                task.Status = "Завършена";
                task.EndDate = DateTime.Now;
                task.Workers?.Clear();
            }

            await repo.SaveChangesAsync();
        }

        public async Task DeleteTask(int id)
        {
            await repo.DeleteAsync<WorkTask>(id);
            await repo.SaveChangesAsync();
        }
    }
}
