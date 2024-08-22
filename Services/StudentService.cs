using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigmaBackend.Models;


namespace SigmaBackend.Services
{
    public class StudentService : IStudentService
    {
        SigmaContext _sigmaContext;
        public StudentService(SigmaContext sigmaContext)
        {
            _sigmaContext = sigmaContext;
        }

        public async Task<Student> Get(Guid id)
        {
            return await _sigmaContext.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _sigmaContext.Students.ToListAsync();
        }

        public async Task Save(Student student)
        {
            _sigmaContext.Students.Add(student);
            await _sigmaContext.SaveChangesAsync();
        }

        public async Task<bool> Update(Guid id, Student student)
        {
            var studenToEdit = await _sigmaContext.Students.FindAsync(id);

            if (studenToEdit != null)
            {
                studenToEdit.StudentName = student.StudentName;
                studenToEdit.StudentLastName = student.StudentLastName;
                studenToEdit.StudentAge = student.StudentAge;
                studenToEdit.UserName = student.UserName;
                studenToEdit.Password = student.Password;
                await _sigmaContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid id)
        {
            var studenToDelete = await _sigmaContext.Students.FindAsync(id);

            if (studenToDelete != null)
            {
                _sigmaContext.Remove(studenToDelete);
                await _sigmaContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

    public interface IStudentService
    {
        Task<Student> Get(Guid id);
        Task<IEnumerable<Student>> GetAll();
        Task Save(Student student);
        Task<bool> Update(Guid id, Student student);
        Task<bool> Delete(Guid id);
    }
}
