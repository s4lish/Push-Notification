using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PushNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }
        // GET: api/<controller>
        [AllowAnonymous]
        [HttpPost("check")]
        public IActionResult Login([FromForm]UserModel login)
        {
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                
                return Ok(new {token = tokenString });
            }

            return Unauthorized(new { token = "" });
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //var secretKey2 = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            //var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey2), SecurityAlgorithms.HmacSha256Signature);

            //var encryptionkey = Encoding.UTF8.GetBytes("ArShaMUrMia_!@#$ArShaMUrMia_!@#$"); //must be 16 character 128 --- 32 for 256
            //var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);

            //var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            //  _config["Jwt:Issuer"],
            //  null,
            //  expires:DateTime.UtcNow.AddHours(14),

            //  signingCredentials: signingCredentials);

            //var claims = new List<Claim>
            //    {
            //       new Claim(ClaimTypes.Name, "UserName"), //user.UserName
            //       new Claim(ClaimTypes.NameIdentifier, "123"), //user.Id
            //    };

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                IssuedAt = DateTime.Now,
                
                //NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddHours(13),
                SigningCredentials = signingCredentials,
                //EncryptingCredentials = encryptingCredentials,
                //Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);
            string encryptedJwt = tokenHandler.WriteToken(securityToken);

            return encryptedJwt;
        }

        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            if (login.Username == "arsham" && login.password == "ali@1368")
            {
                user = new UserModel { Username = "arsham" };
            }
            return user;
        }
    }

    public class UserModel
    {

        public string Username { get; set; }
        public string password { get; set; }
    }
}
