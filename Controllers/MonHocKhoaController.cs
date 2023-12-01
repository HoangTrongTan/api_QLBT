﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webquanlybaithi.Entities;
using Webquanlybaithi.Respositories;

namespace Webquanlybaithi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonHocKhoaController : ControllerBase
    {
        private readonly IMonHocKhoaRespositories resp;

        public MonHocKhoaController(IMonHocKhoaRespositories resp)
        {
            this.resp = resp;
        }
        [HttpGet]
        public async Task<ActionResult> all()
        {
            try
            {
                return Ok(await resp.all());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("find/{name}")]
        public async Task<ActionResult> findName([FromRoute] string name)
        {
            try
            {
                return Ok(await resp.findName(name)  );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Post(Hocphan model)
        {
            try
            {
                return Ok(await resp.post(model));
            }
            catch (Exception ex)
            {
                var str = ex.Message;
                if (ex.InnerException != null)
                {
                    str = ex.InnerException.Message;
                }
                return BadRequest(str);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(Hocphan model)
        {
            try
            {
                return Ok(await resp.put(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{ma}")]
        public async Task<ActionResult> Del([FromRoute] string ma)
        {
            try
            {
                return Ok(await resp.del(ma));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
