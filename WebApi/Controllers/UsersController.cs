using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    private readonly SignInManager<AppUser> _signInManager;

    private readonly IConfiguration _configuration;

    public  UsersController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDTO userDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }


        if (userDTO == null)
        {
            return BadRequest();
        }

        var user = new AppUser
        {
            UserName = userDTO.UserName,
            FullName = userDTO.FullName,
            Email = userDTO.Email,
            DateAdded = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, userDTO.Password);

        if (result.Succeeded)
        {
            return StatusCode(201);
        }

        return BadRequest(result.Errors);

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByNameAsync(loginDTO.UserName);

        if (user == null)
        {
            return BadRequest("Invalid Username or Password");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

        if (result.Succeeded)
        {
            return Ok(new {token = GenerateJWT(user)});
        }

        return Unauthorized();
    }

    private object GenerateJWT(AppUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? "");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? "")
            }),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
