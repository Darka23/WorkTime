using Microsoft.EntityFrameworkCore;
using WorkTime.Contracts;
using WorkTime.Data.Identity;
using WorkTime.Data.Models;
using WorkTime.Data.Repositories;

namespace WorkTime.Services
{

    public class UserServices : IUserServices
    {
        private IApplicationDbRepository repo;
        private IWorkTaskServices workTaskServices;
        public UserServices(IApplicationDbRepository _repo, IWorkTaskServices _workTaskServices)
        {
            repo = _repo;
            workTaskServices = _workTaskServices;
        }

        public Dictionary<ApplicationUser, List<WorkTask>> GetUsers()
        {
            var result = new Dictionary<ApplicationUser, List<WorkTask>>();

            var users = repo.All<ApplicationUser>().ToList();
            var tasks = workTaskServices.GetWorkTasks().ToList();

            foreach (var user in users)
            {
                var userTasks = new List<WorkTask>();

                foreach (var task in tasks)
                {
                    if (task.CreatorId == user.Id)
                    {
                        userTasks.Add(task);
                    }
                }
                result.Add(user, userTasks);
            }

            return result;
        }

        public async Task<bool> EditUser(ApplicationUser model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);


            if (user != null)
            {
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Position = model.Position;
                user.PhoneNumber = model.PhoneNumber;
                user.ImageUrl = model.ImageUrl;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public ApplicationUser PlaceholderUser(string id)
        {
            return repo.All<ApplicationUser>()
                .Where(r => r.Id == id)
                .Select(r => new ApplicationUser()
                {
                    Id = id,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Position = r.Position,
                    PhoneNumber = r.PhoneNumber,
                    Email = r.Email,
                    ImageUrl = r.ImageUrl,
                    
                })
                .First();
        }

        public async Task<ApplicationUser> UserProfileInfo(string id)
        {
            return await repo.GetByIdAsync<ApplicationUser>(id);
        }
    }
}
