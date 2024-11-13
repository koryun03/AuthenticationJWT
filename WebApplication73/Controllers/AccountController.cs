using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication73.Entities;
using WebApplication73.Infrastructure.Jwt;
using WebApplication73.Infrastructure.PasswordHasher;

namespace WebApplication73.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        public AccountController(ApplicationContext context, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        [HttpPost("Register")]
        public IActionResult Register(string login, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);
            var user = new User { Login = login, HashedPassword = hashedPassword };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        //[HttpPost("Login")]
        //public IActionResult Login(User u, HttpContext httpContext)
        //{
        //    var user = _context.Users.FirstOrDefault(p => p.Login == u.Login);
        //    var result = _passwordHasher.Verify(u.HashedPassword, user.HashedPassword);

        //    if (result == false)
        //    {
        //        throw new Exception("failed to login");
        //    }

        //    var token = _jwtProvider.GenerateToken(u);

        //    httpContext.Response.Cookies.Append("testttcoookie", token);

        //    return Ok(token);
        //}

        [HttpPost("Login")]
        public IActionResult Login(User u)
        {
            var user = _context.Users.FirstOrDefault(p => p.Login == u.Login);
            var result = _passwordHasher.Verify(u.HashedPassword, user.HashedPassword);

            if (result == false)
            {
                throw new Exception("failed to login");
            }

            var token = _jwtProvider.GenerateToken(u);

            HttpContext.Response.Cookies.Append("testttcoookie", token);

            return Ok(token);
        }

        [Authorize]
        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("test");
        }


        [Authorize]
        [HttpGet("Cookiijnjel")]
        public IActionResult Cookiijnjel()
        {
            HttpContext.Response.Cookies.Delete("testttcoookie");

            return Ok("Jnjvav");
        }
    }
}
