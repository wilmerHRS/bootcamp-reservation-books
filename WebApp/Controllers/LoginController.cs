using BookServiceReference;
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
                new Claim(JwtRegisteredClaimNames.Name, user.VarFirstName),
                new Claim(JwtRegisteredClaimNames.Sub, user.VarLastName),
				new Claim(JwtRegisteredClaimNames.Email, user.VarEmail),
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
    }
}
