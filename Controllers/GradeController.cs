using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigmaBackend.Models;
using SigmaBackend.Services;

namespace SigmaBackend.Controllers
{
    [EnableCors("CorsRules")]
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        public readonly SigmaContext _sigmaContext;

        public GradeController(IGradeService gradeService, SigmaContext sigmaContext)
        {
            _gradeService = gradeService;
            _sigmaContext = sigmaContext;
        }

        [HttpGet("GetGradetInfo/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var grade = await _gradeService.Get(id);
                if (grade == null)
                {
                    return NotFound("Info not found");
                }

                return Ok(grade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetAllGrades")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var grades = await _gradeService.GetAll();
                if (grades == null || !grades.Any())
                {
                    return NotFound("No grades found");
                }

                return Ok(grades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveGradeInfo")]
        public async Task<IActionResult> Save([FromBody] GradeDTO gradeDto)
        {
            try
            {
                var grade = new Grade
                {
                    GradeId = gradeDto.GradeId,
                    TeacherId = gradeDto.TeacherId,
                    StudentId = gradeDto.StudentId,
                    SubjectId = gradeDto.SubjectId,
                    Grade1 = gradeDto.Grade1,
                    GradeType = gradeDto.GradeType,
                    Student = await _sigmaContext.Students.FindAsync(gradeDto.StudentId),
                    Teacher = await _sigmaContext.Teachers.FindAsync(gradeDto.TeacherId),
                    Subject = await _sigmaContext.Subjects.FindAsync(gradeDto.SubjectId)
                };
                await _gradeService.Save(grade);
                return Ok("Info saved!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateGradeInfo/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GradeDTO gradeDto)
        {
            try
            {
                var grade = new Grade
                {
                    GradeId = gradeDto.GradeId,
                    TeacherId = gradeDto.TeacherId,
                    StudentId = gradeDto.StudentId,
                    SubjectId = gradeDto.SubjectId,
                    Grade1 = gradeDto.Grade1,
                    GradeType = gradeDto.GradeType,
                    Student = await _sigmaContext.Students.FindAsync(gradeDto.StudentId),
                    Teacher = await _sigmaContext.Teachers.FindAsync(gradeDto.TeacherId),
                    Subject = await _sigmaContext.Subjects.FindAsync(gradeDto.SubjectId)
                };
                var proof = await _gradeService.Update(id, grade);
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

        [HttpDelete("DeleteGradeInfo/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var proof = await _gradeService.Delete(id);
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
