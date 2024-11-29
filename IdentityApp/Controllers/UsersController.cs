using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IdentityApp.Controllers;

[Authorize (Roles = "admin")]
public class UsersController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public UsersController(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }


    public IActionResult Index()
    {
        return View(_userManager.Users);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        AppUser user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {   
            ViewBag.Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return View(new EditViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                SelectedRoles = await _userManager.GetRolesAsync(user)
            });
        }

        return RedirectToAction("Index");

    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditViewModel model, string id)
    {
        if(id != model.Id)
        {
            return RedirectToAction("Index");
        }

        if (ModelState.IsValid)
        {
            AppUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.FullName = model.FullName;
                user.Email = model.Email;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {   
                    await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                    if (model.SelectedRoles != null)
                    {
                    await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                    }


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
            
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {

        AppUser user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        else
        {
            ModelState.AddModelError("", "User Not Found");
        }

        return RedirectToAction("Index");
    }
}