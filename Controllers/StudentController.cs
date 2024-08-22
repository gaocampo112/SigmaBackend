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
            try
            {
                var student = await _studentService.Get(id);
                if (student == null)
                {
                    return NotFound("Info not found");
                }

                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var students = await _studentService.GetAll();
                if (students == null || !students.Any())
                {
                    return NotFound("No students found");
                }

                return Ok(students);
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
            try
            {
                var proof = await _studentService.Update(id, student);
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

        [HttpDelete("DeleteStudentInfo/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var proof = await _studentService.Delete(id);
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
