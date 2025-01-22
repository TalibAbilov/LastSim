using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KiderApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
