using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using BlogApp.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace BlogApp.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Logout(string tag)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }



        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var controlEmail = await _userRepository.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
                var controlUserName = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName);
                if (controlUserName != null || controlEmail != null)
                {
                    ModelState.AddModelError("", "Bu email veya kullanıcı adı zaten kullanılmakta");
                    return View(model);
                }

                else
                {

                    var user = new User
                    {
                        Name = model.Name,
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = model.Password
                    };
                    _userRepository.CreateUser(user);
                    return RedirectToAction("Login");

                }


            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Posts");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isUser = _userRepository.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

                if (isUser != null)
                {
                    var userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));

                    if (isUser.Email == "alperen@mail.com")
                    {
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }
                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Index", "Posts");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                }
            }
            return View(model);
        }

        public IActionResult Profile(string userName)
        {   
            if(userName == null)
            {
                return NotFound();
            }

            var user = _userRepository.Users.Include(x=>x.Posts).Include(x=>x.Comments).ThenInclude(x=>x.Post).FirstOrDefault(x=>x.UserName == userName);

            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }


    }

}