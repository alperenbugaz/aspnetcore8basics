using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers;

public class AccountController : Controller
{   
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager; 

    private readonly IEmailSender _emailSender;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager,IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _emailSender = emailSender;
    }

    public IActionResult Login()
    {   
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {   
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {   
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false); 

                if(!user.EmailConfirmed)
                {
                    ModelState.AddModelError("", "Email adresinizi onaylayın");

                    return View(model);
                }

                if (result.Succeeded)
                {   
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Parola yanlış");
                }
            }
            ModelState.AddModelError("", "Email adresi bulunamadı");
        }
        return View(model);


    }

        [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize (Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            AppUser user = new AppUser
            {
                UserName = model.FullName,
                Email = model.Email,
                FullName = model.FullName

            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {   
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new { user.Id,token });

                await _emailSender.SendEmailAsync(user.Email, "Hesabınızı Onaylayın", $"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:5059{url}'>tıklayınız</a>");

                TempData[( "message" )] = "Lütfe email hesabınızı onaylayın";
                return RedirectToAction("Login","Account");
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

    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (userId == null || token == null)
        {   
            TempData["message"] = "Geçersiz token";
            return  View();
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {           
                TempData["message"] = "Hesabınız Onaylandı";

                return View();
            }
        }

        return View("Kullanıcı Bulunamadı");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {   
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string email)
    {   
        if (string.IsNullOrEmpty(email))
        {
            TempData["message"] = "Email adresi girmelisiniz";
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user != null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword", "Account", new { email = email, token = token });

            await _emailSender.SendEmailAsync(email, "Parola Sıfırlama", $"Parolanızı sıfırlamak için linke <a href='http://localhost:5059{url}'>tıklayınız</a>");

            TempData["message"] = "Parola sıfırlama linki email adresinize gönderildi";
            return View();
        }
        else
        {
            TempData["message"] = "Email adresi ile eşleşen kullanıcı bulunamadı";
            return View();
        }

        TempData["message"] = "Email adresi bulunamadı";
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string email, string token)
    {
        if (email == null || token == null)
        {
            TempData["message"] = "Geçersiz token";
            return View();
        }

        var model = new ResetPasswordViewModel { Email = email, Token = token };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    TempData["message"] = "Parola sıfırlandı";
                    return View();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            TempData["message"] = "Kullanıcı bulunamadı";
            return View(model);
        }
        return View(model);
    }
}