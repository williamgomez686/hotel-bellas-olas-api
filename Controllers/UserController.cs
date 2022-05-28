using hotel_bellas_olas_api.Models;
using hotel_bellas_olas_api.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace hotel_bellas_olas_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        BellasOlasHotelDbContext db;
        private readonly IWebHostEnvironment _env;

        public UserController(BellasOlasHotelDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this._env = webHostEnvironment;
        }

        [HttpPost]
        [Route("/API/User/LogInUser")]
        public async Task<IActionResult> Post(UserInfo userInfo)
        {
            var user = this.db.Users.ToList().Where(u => u.UserName.Equals(userInfo.userName) && u.Password.Equals(userInfo.password)).FirstOrDefault();
            if (user != null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.Role, "Admin")
,                };
                var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                null);
            }
            if(user != null)
            {
                return Ok(new{userName= user.UserName,UserId=user.UserId });
            }
            else
            {
                return Ok(new { msg = "Los datos no corresponden a ningún usuario" });
            }
        }
        [HttpPost]
        [Route("/API/User/LogOutUser")]
        public async Task<IActionResult> post()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Sesión cerrada");
        }
    }
}
