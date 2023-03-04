using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BooksMgmt.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;



namespace BooksMgmt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly InMemoryData _data;

        public LoginController(IConfiguration configuration, InMemoryData data)
        {
            _config = configuration;
            _data = data;
        }

        [HttpPost]
        public ActionResult<string> Login([FromBody] UserLogin userLogin)
        {
            var user = _data.Users.FirstOrDefault(x=>x.Password==userLogin.Password && x.Name==userLogin.Username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var token = GenerateToken(user);

            return Ok(token);
        }

        private string GenerateToken(User user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
