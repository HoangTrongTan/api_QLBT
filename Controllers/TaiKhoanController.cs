using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webquanlybaithi.Entities;
using Webquanlybaithi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Webquanlybaithi.Respositories;
using Microsoft.AspNetCore.Authorization;

namespace Webquanlybaithi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITaiKhoanRespositories resp;

        public TaiKhoanController(IConfiguration configuration,ITaiKhoanRespositories respositories) 
        { 
            _configuration = configuration;
            resp = respositories;
        }
        [HttpPost("register")]
        public async Task<ActionResult<Taikhoan>> Register(Taikhoan tk)
        {
            try
            {

                return Ok(await resp.register(tk));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(Taikhoan tk)
        {
            var tokken = await resp.getUser(tk);
            if (tokken ==  null)
            {
                return BadRequest("Tài khoản không tồn tại");
            }else
            {
                return Ok(tokken);
            }
        }
        
        [HttpGet("get-user-info")]
        [Authorize] // Bảo vệ endpoint này với JWT Authorization
        public ActionResult<string> GetUserInfo()
        {
            try
            {
                // Lấy thông tin từ Principal của người dùng hiện tại (đã được xác minh qua JWT)
                var username = User.Identity.Name; // ClaimTypes.Name
                var maKhoa = User.FindFirst("MaKhoa")?.Value;
                var tenDangNhap = User.FindFirst("TenDangNhap")?.Value;
                var maUser = User.FindFirst("maUser")?.Value;
                var maLopds = User.FindFirst("maLopds")?.Value;
                var maKhoaDk = User.FindFirst("maKhoaDk")?.Value;

                // Bạn có thể sử dụng các thông tin này theo cách bạn muốn
                // Ví dụ: trả về JSON chứa thông tin người dùng
                var userInfo = new
                {
                    Username = username,
                    MaKhoa = maKhoa,
                    TenDangNhap = tenDangNhap,
                    MaUser = maUser,
                    MaLopds = maLopds,
                    MaKhoaDk = maKhoaDk
                };

                return Ok(userInfo);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}