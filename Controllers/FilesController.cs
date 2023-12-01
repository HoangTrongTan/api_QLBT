using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webquanlybaithi.Entities;
using Webquanlybaithi.Models;
using Webquanlybaithi.Respositories;

namespace Webquanlybaithi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesRespositories _resp;
        public FilesController(IFilesRespositories resp)
        {
            _resp = resp;
        }
        [HttpGet("{ma}")]
        public async Task<ActionResult> GetFiles(string ma)
        {
            try
            {
                return Ok(await _resp.get(ma) );
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] FilesUpModel model) 
        {
            try
            {
                return Ok(await _resp.post(model) );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Del(int id)
        {
            try
            {
                return Ok( await _resp.delete(id) );
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> FixFiles([FromForm]FilesUpModel model)
        {
            try
            {
                return Ok( await _resp.Fix(model) );
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
