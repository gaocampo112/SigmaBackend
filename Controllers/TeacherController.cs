using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigmaBackend.Models;
using SigmaBackend.Services;
using System.Threading;
using Microsoft.AspNetCore.Cors;

namespace SigmaBackend.Controllers
{
    [EnableCors("CorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        public readonly SigmaContext _sigmaContext;

        public TeacherController(SigmaContext sigmaContext, ITeacherService teacherService)
        {
            _sigmaContext = sigmaContext;
            _teacherService = teacherService;
        }

        [HttpGet("GetTeacherInfo/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var teacher = await _teacherService.Get(id);
                if (teacher == null)
                {
                    return NotFound("Info not found");
                }

                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("SaveTeacherInfo")]
        public async Task<IActionResult> Save([FromBody] Teacher teacher)
        {
            try
            {
                await _teacherService.Save(teacher);
                return Ok("Info saved!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateTeacherInfo/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Teacher teacher)
        {
            try
            {
                var proof = await _teacherService.Update(id, teacher);
                if (proof == true)
                {
                    return Ok("Info saved!");
                }
                else
                {
                    return NotFound("Info not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteTeacherInfo/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var proof = await _teacherService.Delete(id);
            try
            {
                if (proof == true)
                {
                    return Ok("Info Deleted!");
                }
                else
                {
                    return NotFound("Info not found");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
