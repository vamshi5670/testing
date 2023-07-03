using JWTToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTToken.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : Controller
    {
       private readonly IConfiguration _config;

        public LoginController(IConfiguration config) 
        {
            _config = config;        
        }

        //private readonly JwttokenContext _context;

        //public LoginController(JwttokenContext context) 
        //{
        //    _context = context;
        //}
       

        [AllowAnonymous]
        [HttpPost]




        //public async Task<UserModel> GetUserCredentialsAsync(UserLogin userLogin)
        //{
        //    using (var context = new JwttokenContext())
        //    {
        //        var user = await context.userModels.SingleOrDefaultAsync(u => u.Username == userLogin.Username && u.Password == userLogin.Password);
        //        if (user == null)
        //        {
        //            return null;

        //        }

        //        return user;
        //    }
        //}


        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return NotFound("user not found");
        }



        //to generate token
        private string GenerateToken(UserModel user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role,user.Role),
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                        _config["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(1),
                        signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //To authenticate user

        private UserModel Authenticate(UserLogin userLogin)
        {

            var currentUser = UserConstrants.Users.FirstOrDefault(x => x.Username.ToLower() == 
                                        userLogin.Username.ToLower() && x.Password == userLogin.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }


    }
}




//public async Task<IActionResult> Login([FromBody] UserLogin req)
//{
//    var (username, password) = await GetUserCredentialsAsync(req.Username);
//    if (username == null || password != req.Password)
//    {
//        return Unauthorized();
//    }


//    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
//    var claims = new[]
//    {
//        new Claim(ClaimTypes.NameIdentifier,req.Username)

//    };
//    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
//                _config["Jwt:Audience"],
//                claims,
//                expires: DateTime.Now.AddMinutes(1),
//                signingCredentials: credentials);

//    return new JwtSecurityTokenHandler().WriteToken(token);
//}


//public async Task<(string username, string password)> GetUserCredentialsAsync(string username)
//{
//    using (var context = new JwttokenContext())
//    {
//        var user = await context.userModels.SingleOrDefaultAsync(u => u.Username == username);
//        if (user == null)
//        {
//            return (null, null);
//        }

//        return (user.Username, user.Password);

//    }
//}