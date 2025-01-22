using AutoMapper;
using KiderApp.Areas.Manage.ViewModels.Designation;
using KiderApp.DAL;
using KiderApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KiderApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]

    public class DesignationController(AppDbContext _db,IMapper _mapper) : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            var designations=await _db.Designations.Include(x=>x.Agents).ToListAsync();
            return View(designations);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(CreateDesignationVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _db.Designations.AnyAsync(x =>x.Name == vm.Name))
            {
                ModelState.AddModelError("Name", "Bu name movcuddur!");
                return View();
            }
            var newDesignation=_mapper.Map<Designation>(vm);
            await _db.Designations.AddAsync(newDesignation);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Update(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            var designation=await _db.Designations.FirstOrDefaultAsync(x => x.Id==id);
            if (designation == null)
            {
                return NotFound();
            }
            UpdateDesignationVm vm=_mapper.Map<UpdateDesignationVm>(designation);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult>Update(UpdateDesignationVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if(await _db.Designations.AnyAsync(x => x.Id!=vm.Id && x.Name == vm.Name))
            {
                ModelState.AddModelError("Name", "Bu name movcuddur!");
                return View(vm);
            }
            var designation= await _db.Designations.FirstOrDefaultAsync(x=>x.Id==vm.Id);
            if (designation == null)
            {
                return NotFound();
            }
            _mapper.Map(vm, designation);   
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult>Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var designation = await _db.Designations.FirstOrDefaultAsync(x => x.Id == id);
            if (designation == null)
            {
                return NotFound();
            }
            _db.Designations.Remove(designation);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
       
    }
}
