using KiderApp.Helpers.Enums;
using KiderApp.Models;
using KiderApp.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace KiderApp.Controllers
{
    public class AccountController(UserManager<AppUser>_userManager,SignInManager<AppUser>_signInManager,RoleManager<IdentityRole>_roleManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user=new AppUser()
            {
                UserName=vm.FirstName+vm.LastName,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
            };
            var result=await _userManager.CreateAsync(user,vm.Password);
            if(!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user,UserRoles.User.ToString());

            return RedirectToAction("Login");
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach(var item in Enum.GetValues(typeof(UserRoles)))
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = item.ToString(),
                });
            }
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm,string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user = await _userManager.FindByEmailAsync(vm.EmailOrUserName)??await _userManager.FindByNameAsync(vm.EmailOrUserName);
            if (user == null)
            {
                ModelState.AddModelError("", "EmailOrUserName ve ya Password sehvdir!");
                return View(vm);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, vm.Password, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan sinayin!");
                return View(vm);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "EmailOrUserName ve ya Password sehvdir!");
                return View(vm);
            }
            await _signInManager.SignInAsync(user, vm.Reminder);
            if(ReturnUrl != null)
            {
                return Redirect(ReturnUrl);    
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
