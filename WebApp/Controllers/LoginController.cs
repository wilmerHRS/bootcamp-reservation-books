using BookServiceReference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserServiceReference;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserServiceClient userService = new UserServiceClient();
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate(CredentialRequestDto credential)
        {
            var user = await userService.LoginAsync(credential);

            if (user == null) return Unauthorized(new { message = "Not authorized" });

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
			{
                new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Sub, user.LastName),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.IdUser.ToString())
			};

            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddDays(2),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(new
            {
                AccessToken = token,
                User = user,
            });
        }

        [Authorize]
        public ActionResult verifyToken()
        {
            var user = new UserResponseDto
            {
                IdUser = Convert.ToInt32(User.FindFirstValue(JwtRegisteredClaimNames.Jti)),
                FirstName = User.FindFirstValue(JwtRegisteredClaimNames.Name),
                LastName = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Email = User.FindFirstValue(ClaimTypes.Email),
                Status = 1
            };

            return Ok(user);
        }
    }
}
