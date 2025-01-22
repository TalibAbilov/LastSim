using AutoMapper;
using KiderApp.Areas.Manage.Helpers.Extensions;
using KiderApp.Areas.Manage.ViewModels.Agent;
using KiderApp.Areas.Manage.ViewModels.Designation;
using KiderApp.DAL;
using KiderApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KiderApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin")]
    public class AgentController(AppDbContext _db, IMapper _mapper,IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var agents=await _db.Agents.Include(x=>x.Designation).ToListAsync();
            return View(agents);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Designations=await _db.Designations.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAgentVm vm)
        {
            ViewBag.Designations = await _db.Designations.ToListAsync();
            if(vm.file == null)
            {
                ModelState.AddModelError("file", "Fayl secc!!");
                return View();
            }
            if (await _db.Agents.AnyAsync(x => x.FullName == vm.FullName))
            {
                ModelState.AddModelError("FullName", "Bu fullname movcuddur!");
                return View();
            }
            if (!vm.file.ContentType.Contains("image"))
            {
                ModelState.AddModelError("file", "Duzgun fayl tipi sec!");
                return View();
            }
            if (vm.file.Length>2097220)
            {
                ModelState.AddModelError("file", "Max 2mb fayl sec!");
                return View();
            }
            vm.ImgUrl = vm.file.Upload(_env.WebRootPath, "Upload/Agent");
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newAgent = _mapper.Map<Agent>(vm);
            await _db.Agents.AddAsync(newAgent);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Designations = await _db.Designations.ToListAsync();

            if (id == null)
            {
                return BadRequest();
            }
            var agent = await _db.Agents.FirstOrDefaultAsync(x => x.Id == id);
            if (agent == null)
            {
                return NotFound();
            }
            UpdateAgentVm vm = _mapper.Map<UpdateAgentVm>(agent);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateAgentVm vm)
        {
            ViewBag.Designations = await _db.Designations.ToListAsync();

            var agent = await _db.Agents.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (agent == null)
            {
                return NotFound();
            }
            if (await _db.Agents.AnyAsync(x => x.Id != vm.Id && x.FullName == vm.FullName))
            {
                ModelState.AddModelError("FullName", "Bu name movcuddur!");
                return View(vm);
            }
            if(vm.file==null)
            {
                vm.ImgUrl=agent.ImgUrl;
            }
            else
            {
                if (!vm.file.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("file", "Duzgun fayl tipi sec!");
                    return View();
                }
                if (vm.file.Length > 2097220)
                {
                    ModelState.AddModelError("file", "Max 2mb fayl sec!");
                    return View();
                }
                vm.ImgUrl=vm.file.Upload(_env.WebRootPath, "Upload/Agent");
                FileExtension.Delete(_env.WebRootPath,"Upload/Agent",agent.ImgUrl);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            _mapper.Map(vm, agent);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var agent = await _db.Agents.FirstOrDefaultAsync(x => x.Id == id);
            if (agent == null)
            {
                return NotFound();
            }
            FileExtension.Delete(_env.WebRootPath, "Upload/Agent", agent.ImgUrl);
            _db.Agents.Remove(agent);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
