using ComputerStore.BLL.Models.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IConfiguration configuration, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"User '{userExists.UserName}' already exists");
            }

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);



            return Ok($"User '{user.UserName}' registered successfully");
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);

            if (userExists == null || !await _userManager.CheckPasswordAsync(userExists, model.Password))
            {
                return Unauthorized();
            }

            return Ok(GenerateJwtToken(userExists, userExists.Email));

        }

        private string GenerateJwtToken(IdentityUser user, string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpireDays"]));

            var authorizationClaims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Exp, expires.ToString(CultureInfo.InvariantCulture))

            };

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                authorizationClaims,
                expires: expires,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.AsNoTracking().ToArrayAsync();

            return Ok(users);
        }
    }
}
