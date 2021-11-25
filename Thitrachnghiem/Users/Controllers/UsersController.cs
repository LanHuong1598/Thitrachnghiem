using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Functions;
using Thitrachnghiem.Users.Models.Entities;
using Thitrachnghiem.Users.Services;
using Microsoft.AspNetCore.Authorization;
using Thitrachnghiem.Users.Models.Schema;
using PagedList;

namespace Thitrachnghiem.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUsersService usersService;

        public UsersController(IUsersService usersService_)
        {
            usersService = usersService_;
        }

        // GET: api/Users
        /// <summary>
        /// get all users 
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin,USER_GET")]
        public async Task<ActionResult> GetUsers([FromQuery] Pageing pageing, string keyword)
        {
            Pair<List<UserGet>, int> users = usersService.GetUsers(keyword, pageing);
            return Ok(new
            {
                header = new Header(users.Second, pageing.offset, pageing.limit, "true"),
                body = users.First
            });
        }

        

        /// <summary>
        /// get a user by uuid
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        [Authorize(Roles = "admin,USER_GET")]
        public async Task<ActionResult> GetUser(string uuid)
        {
            UserGet user = usersService.GetUsersByUuid(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        /// <summary>
        /// them 1 user 
        /// </summary>
        /// <param name="userCreate"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,USER_ADD")]
        public async Task<ActionResult<UserGet>> ThemUser([FromBody] UserCreate userCreate)
        {
            UserGet user = usersService.Create(userCreate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        /// <summary>
        /// sua 1 user 
        /// </summary>
        /// <param name="userUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin,USER_EDIT")]
        public async Task<ActionResult<UserGet>> SuaUser([FromBody] UserUpdate userUpdate)
        {
            UserGet user = usersService.Update(userUpdate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        /// <summary>
        /// xóa 1 user 
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpDelete("{uuid}")]
        [Authorize(Roles = "admin,USER_DELETE")] 
        public async Task<ActionResult<UserGet>> XoaUser(string uuid)
        {
            UserGet user = usersService.Delete(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        // quyen
        /// <summary>
        /// lay danh sach nhom quyen
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Role")]
        [Authorize(Roles = "admin,ROLE_GET")]
        public async Task<ActionResult<Role>> GetRoles([FromQuery] Pageing pageing)
        {            
            var roles = usersService.GetRoles();
            if (roles == null)
                return Ok(new
                {
                    header = new Header(0, pageing.offset, pageing.limit, "true"),
                    body = ""
                }); 

            return Ok(new
            {
                header = new Header(roles.Count, pageing.offset, pageing.limit, "true"),
                body = roles
            }); 
        }

        // quyen
        /// <summary>
        /// lay danh sach cac quyen trong 1 nhom quyen
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Authorities")]
        [Authorize(Roles = "admin,ROLE_GET")]
        public async Task<ActionResult<Authority>> GetAuthority([FromQuery] Pageing pageing)
        {
            List<Authority> roles = new List<Authority>();
            roles = usersService.GetAuthorities();

            return Ok(new
            {
                header = new Header(roles.Count, pageing.offset, pageing.limit, "true"),
                body = roles
            }); ;
        }

        /// <summary>
        /// lay 1 quyen bang uuid
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet("Role/{uuid}")]
        [Authorize(Roles = "admin,ROLE_GET")]

        public async Task<ActionResult> GetRoleByUUId(Guid uuid)
        {
            Role user = usersService.GetRoleByUuid(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        /// <summary>
        /// Them 1 nhom quyen
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("Role/")]
        [Authorize(Roles = "admin,ROLE_ADD")]
        public async Task<ActionResult<Role>> AddRole([FromBody] RoleCreate role)
        {
            Role user = usersService.CreateRole(role);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        /// <summary>
        /// sua 1 nhom quyen
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut("Role/")]
        [Authorize(Roles = "admin,ROLE_EDIT")]
        public async Task<ActionResult<Role>> UpdateRole([FromBody] RoleUpdate role)
        {
            Role user = usersService.UpdateRole(role);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        /// <summary>
        /// xoa 1 nhomquyen
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpDelete("Role/{uuid}")]
        [Authorize(Roles = "admin,ROLE_DELETE")]
        public async Task<ActionResult<Role>> DeleteRole(Guid uuid)
        {
            Role user = usersService.DeleteRole(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        ///// <summary>
        ///// Them 1 role vào user 
        ///// </summary>
        ///// <param name="u"></param>
        ///// <returns></returns>
        //[HttpPost("UserRole/")]
        //[Authorize(Roles = "admin,ROLE_ADD")]
        //public async Task<ActionResult<Userrole>> AddUserRole([FromBody] UserRoleCreate u)
        //{
        //    UsersService f_Users = new UsersService();
        //    Userrole user = f_Users.CreateUserRole(u);
        //    return Ok(new
        //    {
        //        header = new Header(1, 0, 1, "true"),
        //        body = true
        //    });
        //}

        /// <summary>
        /// SƯA DANH SÁCH QUYỀN CỦA USER
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [HttpPut("UserRole/")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Userrole>> UpdateUserRole([FromBody] UserRoleUpdate u)
        {
            UserGet user = usersService.UpdateUserRole(u);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }


        //[HttpDelete("UserRole/")]
        //[Authorize(Roles = "admin")]
        //public async Task<ActionResult<Userrole>> DeleteUserRole([FromBody] UserRoleCreate u)
        //{
        //    UsersService f_Users = new UsersService();
        //    Userrole user = f_Users.DeleteUserRole(u);
        //    return Ok(new
        //    {
        //        header = new Header(1, 0, 1, "true"),
        //        body = true
        //    });
        //}
    }
}
