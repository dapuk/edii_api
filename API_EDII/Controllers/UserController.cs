using API_EDII.Entities;
using API_EDII.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_EDII.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly EdiiDbContext DBContext;

        public UserController(EdiiDbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<IDataTblUser>>> Get()
        {
            var List = await DBContext.TblUsers.Select(
                s => new IDataTblUser
                {
                    Userid = s.Userid,
                    Namalengkap = s.Namalengkap,
                    Username = s.Username,
                    Password = s.Password,
                    Status = s.Status
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<IDataTblUser>> GetUserById(int Id)
        {
            IDataTblUser User = await DBContext.TblUsers.Select(s => new IDataTblUser
            {
                Userid = s.Userid,
                Namalengkap = s.Namalengkap,
                Username = s.Username,
                Password = s.Password,
                Status = s.Status
            }).FirstOrDefaultAsync(s => s.Userid == Id);
            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return User;
            }
        }

        [HttpPost("InsertUser")]
        public async Task<HttpStatusCode> InsertUser(IDataTblUser User)
        {
            var entity = new TblUser()
            {
                Userid = User.Userid,
                Namalengkap = User.Namalengkap,
                Username = User.Username,
                Password = User.Password,
                Status = User.Status
            };
            DBContext.TblUsers.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(IDataTblUser User)
        {
            var entity = await DBContext.TblUsers.FirstOrDefaultAsync(s => s.Userid == User.Userid);
            entity.Userid = User.Userid;
            entity.Namalengkap = User.Namalengkap;
            entity.Username = User.Username;
            entity.Password = User.Password;
            entity.Status = User.Status;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(int Id)
        {
            var entity = new TblUser()
            {
                Userid = Id
            };
            DBContext.TblUsers.Attach(entity);
            DBContext.TblUsers.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
