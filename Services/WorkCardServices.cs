using Microsoft.AspNetCore.Mvc;
using WorkTime.Contracts;
using WorkTime.Data.Models;
using WorkTime.Data.Repositories;

namespace WorkTime.Services
{
    public class WorkCardServices : IWorkCardServices
    {
        private IApplicationDbRepository repo;
        public WorkCardServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }
        public async Task AddWorkCard([FromForm] WorkCard workCard, int id)
        {
            var task = repo.All<WorkTask>().Where(x => x.Id == id).First();

            await repo.AddAsync(new WorkCard()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Description = workCard.Description,
                WorkTask = task,
                WorkTaskId = task.Id
            });

            await repo.SaveChangesAsync();
        }
    }
}
