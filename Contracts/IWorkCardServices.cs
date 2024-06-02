using Microsoft.AspNetCore.Mvc;
using WorkTime.Data.Models;

namespace WorkTime.Contracts
{
    public interface IWorkCardServices
    {
        Task AddWorkCard([FromForm] WorkCard workCard, int id);
    }
}
