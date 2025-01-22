
using KiderApp.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KiderApp.Controllers
{
    public class HomeController(AppDbContext _db) : Controller
    {
        
        public async Task<IActionResult> Index()
        {   
            var agents=await _db.Agents.Include(x=>x.Designation).ToListAsync();
            return View(agents);
        }
        

    }
}
