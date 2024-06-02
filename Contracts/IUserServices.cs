using WorkTime.Data.Identity;
using WorkTime.Data.Models;

namespace WorkTime.Contracts
{
    public interface IUserServices
    {
        Dictionary<ApplicationUser, List<WorkTask>> GetUsers();
        Task<bool> EditUser(ApplicationUser model);
        ApplicationUser? PlaceholderUser(string id);

        Task<ApplicationUser> UserProfileInfo(string id);
    }
}
