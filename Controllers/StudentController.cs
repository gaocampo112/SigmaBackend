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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public readonly SigmaContext _sigmaContext;

        public StudentController(SigmaContext sigmaContext, IStudentService studentService)
        {
            _sigmaContext = sigmaContext;
            _studentService = studentService;
        }


        [HttpGet("GetStudentInfo/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var student = await _studentService.Get(id);
            if (student == null)
            {
                return NotFound("Info not found");
            }

            try
            {
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        

        [HttpPost("SaveStudentInfo")]
        public async Task<IActionResult> Save([FromBody] Student student)
        {
            try
            {
                await _studentService.Save(student);
                return Ok("Info saved!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateStudentInfo/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Student student)
        {
            var proof = await _studentService.Update(id, student); ; 
            try
            {
                if(proof == true)
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

        [HttpDelete("DeleteStudentInfo/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var proof = await _studentService.Delete(id);
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
