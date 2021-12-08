using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thitrachnghiem.Services;
using Thitrachnghiem.Users.Models.Schema;
using Thitrachnghiem.Users.Services;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Thitrachnghiem.Controllers
{
    [Route("api/home/")]
    public class LoginController : ControllerBase
    {
        IUsersService usersService;
        public LoginController(IUsersService usersService_)
        {
            usersService = usersService_;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserLogin userlogin)
        {
            try
            {
                UsersService usersService = new UsersService();
                var user = usersService.Login(userlogin);

                if (user == null)
                    return NotFound(new { message = "User or password invalid" });

                var token = TokenService.CreateToken(user);
                return new
                {
                    user = user,
                    token = token
                };
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


      

    }
}
