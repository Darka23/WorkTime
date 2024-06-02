using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorkTime.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
