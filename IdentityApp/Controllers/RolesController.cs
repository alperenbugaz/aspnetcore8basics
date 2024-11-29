using IdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Controllers;

[Authorize (Roles = "admin")]
public class RolesController:Controller
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public RolesController(RoleManager<AppRole> roleManager,UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;


    }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleUsers = new Dictionary<string, List<string>>();
            foreach (var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                roleUsers[role.Id] = usersInRole.Select(u => u.UserName).ToList();
            }

            ViewBag.RoleUsers = roleUsers;
            return View(roles);
        }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(AppRole model)
    {   
        if(ModelState.IsValid)
        {        
        IdentityResult result = await _roleManager.CreateAsync(model);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);

        }
        return View(model);

    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {   
        AppRole role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {   
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                ViewBag.Users = usersInRole;
            return View(role);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(AppRole model)
    {
        if (ModelState.IsValid)
        {
            IdentityResult result = await _roleManager.UpdateAsync(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return View(model);
    }
}